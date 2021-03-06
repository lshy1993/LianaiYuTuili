/**
 *
 *  You can modify and use this source freely
 *  only for the development of application related Live2D.
 *
 *  (c) Live2D Inc. All rights reserved.
 */

using System.Collections;
using System.Collections.Generic;
using live2d ;

namespace live2d.framework
{
    /*
     * パーツの切り替えを管理する。
     *
     */
    public class L2DPose
    {

        protected List<L2DPartsParam[]> partsGroupList;
        private long lastTime = 0;
        private ALive2DModel lastModel = null;// パラメータインデックスが初期化されてるかどうかのチェック用。


        public L2DPose()
        {
            partsGroupList = new List<L2DPartsParam[]>();
        }


        public void addPartsGroup(L2DPartsParam[] partsGroup)
        {
            partsGroupList.Add(partsGroup);
        }


        public void addPartsGroup(string[] idGroup)
        {
            L2DPartsParam[] partsGroup = new L2DPartsParam[idGroup.Length];

            for (int i = 0; i < idGroup.Length; i++)
            {
                partsGroup[i] = new L2DPartsParam(idGroup[i]);
            }

            partsGroupList.Add(partsGroup);
        }


        /*
         * モデルのパラメータを更新。
         * @param model
         */
        public void updateParam(ALive2DModel model)
        {
            if (model == null) return;

            // 前回のモデルと同じではないので初期化が必要
            if (model != lastModel)
            {
                //  パラメータインデックスの初期化
                initParam(model);
            }
            lastModel = model;

            long curTime = UtSystem.getUserTimeMSec();
            float deltaTimeSec = ((lastTime == 0) ? 0 : (curTime - lastTime) / 1000.0f);
            lastTime = curTime;

            // 設定から時間を変更すると、経過時間がマイナスになることがあるので、経過時間0として対応。
            if (deltaTimeSec < 0) deltaTimeSec = 0;

            for (int i = 0; i < partsGroupList.Count; i++)
            {
                normalizePartsOpacityGroup(model, partsGroupList[i], deltaTimeSec);
                copyOpacityOtherParts(model, partsGroupList[i]);
            }
        }


        /*
         * 表示を初期化。
         * αの初期値が0でないパラメータは、αを1に設定する。
         * @param model
         */
        public void initParam(ALive2DModel model)
        {
            if (model == null) return;

            for (int i = 0; i < partsGroupList.Count; i++)
            {

                L2DPartsParam[] partsGroup = partsGroupList[i];
                for (int j = 0; j < partsGroup.Length; j++)
                {
                    partsGroup[j].initIndex(model);

                    int partsIndex = partsGroup[j].partsIndex;
                    int paramIndex = partsGroup[j].paramIndex;
                    if (partsIndex < 0) continue;// 存在しないパーツです

                    bool v = (model.getParamFloat(paramIndex) != 0);
                    model.setPartsOpacity(partsIndex, (v ? 1.0f : 0.0f));
                    model.setParamFloat(paramIndex, (v ? 1.0f : 0.0f));
                }
            }
        }


        /*
         * パーツのフェードイン、フェードアウトを設定する。
         * @param model
         * @param partsGroup
         * @param deltaTimeSec
         */
        public void normalizePartsOpacityGroup(ALive2DModel model, L2DPartsParam[] partsGroup, float deltaTimeSec)
        {
            int visibleParts = -1;
            float visibleOpacity = 1.0f;

            float CLEAR_TIME_SEC = 0.5f;// この時間で不透明になる
            float phi = 0.5f;// 背景が出にくいように、１＞０への変化を遅らせる場合は、0.5よりも大きくする。ただし、あまり自然ではない
            float maxBackOpacity = 0.15f;


            //  現在、表示状態になっているパーツを取得
            for (int i = 0; i < partsGroup.Length; i++)
            {
                int partsIndex = partsGroup[i].partsIndex;
                int paramIndex = partsGroup[i].paramIndex;

                if (partsIndex < 0) continue;// 存在しないパーツです

                if (model.getParamFloat(paramIndex) != 0)
                {
                    if (visibleParts >= 0)
                    {
                        break;
                    }
                    visibleParts = i;
                    visibleOpacity = model.getPartsOpacity(partsIndex);

                    //  新しいOpacityを計算
                    visibleOpacity += deltaTimeSec / CLEAR_TIME_SEC;
                    if (visibleOpacity > 1)
                    {
                        visibleOpacity = 1;
                    }
                }
            }

            if (visibleParts < 0)
            {
                visibleParts = 0;
                visibleOpacity = 1;
            }

            //  表示パーツ、非表示パーツの透明度を設定する
            for (int i = 0; i < partsGroup.Length; i++)
            {
                int partsIndex = partsGroup[i].partsIndex;
                if (partsIndex < 0) continue;// 存在しないパーツです

                //  表示パーツの設定
                if (visibleParts == i)
                {
                    model.setPartsOpacity(partsIndex, visibleOpacity);// 先に設定
                }
                //  非表示パーツの設定
                else
                {
                    float opacity = model.getPartsOpacity(partsIndex);
                    float a1;// 計算によって求められる透明度
                    if (visibleOpacity < phi)
                    {
                        a1 = visibleOpacity * (phi - 1) / phi + 1; //  (0,1),(phi,phi)を通る直線式
                    }
                    else
                    {
                        a1 = (1 - visibleOpacity) * phi / (1 - phi); //  (1,0),(phi,phi)を通る直線式
                    }

                    // 背景の見える割合を制限する場合
                    float backOp = (1 - a1) * (1 - visibleOpacity);// 背景の
                    if (backOp > maxBackOpacity)
                    {
                        a1 = 1 - maxBackOpacity / (1 - visibleOpacity);
                    }

                    if (opacity > a1)
                    {
                        opacity = a1;//  計算の透明度よりも大きければ（濃ければ）透明度を上げる
                    }
                    model.setPartsOpacity(partsIndex, opacity);
                }
            }
        }


        /*
         * パーツのαを連動する。
         * @param model
         * @param partsGroup
         */
        public void copyOpacityOtherParts(ALive2DModel model, L2DPartsParam[] partsGroup)
        {
            for (int i_group = 0; i_group < partsGroup.Length; i_group++)
            {
                L2DPartsParam partsParam = partsGroup[i_group];

                if (partsParam.link == null) continue;// リンクするパラメータはない
                if (partsParam.partsIndex < 0) continue;// 存在しないパーツ

                float opacity = model.getPartsOpacity(partsParam.partsIndex);

                for (int i_link = 0; i_link < partsParam.link.Count; i_link++)
                {
                    L2DPartsParam linkParts = partsParam.link[i_link];

                    if (linkParts.partsIndex < 0)
                    {
                        //
                        linkParts.initIndex(model);
                    }

                    if (linkParts.partsIndex < 0) continue;//
                    model.setPartsOpacity(linkParts.partsIndex, opacity);
                }
            }
        }

        public static L2DPose load(byte[] buf)
        {
            return load(System.Text.Encoding.GetEncoding("UTF-8").GetString(buf));
        }


        public static L2DPose load(string buf)
        {
            return load(buf.ToCharArray());
        }


        /*
         * JSONファイルから読み込む
         * 仕様についてはマニュアル参照。JSONスキーマの形式の仕様がある。
         * @param buf
         * @return
         */
        public static L2DPose load(char[] buf)
        {
            L2DPose ret = new L2DPose();

            Value json = Json.parseFromBytes(buf);

            // パーツ切り替え一覧
            List<Value> poseListInfo = json.get("parts_visible").getVector(null);
            int poseNum = poseListInfo.Count;

            for (int i_pose = 0; i_pose < poseNum; i_pose++)
            {
                Value poseInfo = poseListInfo[i_pose];

                // IDリストの設定
                List<Value> idListInfo = poseInfo.get("group").getVector(null);
                int idNum = idListInfo.Count;
                L2DPartsParam[] partsGroup = new L2DPartsParam[idNum];
                for (int i_group = 0; i_group < idNum; i_group++)
                {
                    Value partsInfo = idListInfo[i_group];
                    L2DPartsParam parts = new L2DPartsParam(partsInfo.get("id").toString());
                    partsGroup[i_group] = parts;

                    // リンクするパーツの設定
                    if (!partsInfo.getMap(null).ContainsKey("link")) continue;// リンクが無いときもある
                    List<Value> linkListInfo = partsInfo.get("link").getVector(null);
                    int linkNum = linkListInfo.Count;
                    parts.link = new List<L2DPartsParam>();
                    for (int i_link = 0; i_link < linkNum; i_link++)
                    {
                        //					string linkID = idListInfo.get(i_group).tostring();//parts ID
                        L2DPartsParam linkParts = new L2DPartsParam(linkListInfo[i_link].toString());
                        parts.link.Add(linkParts);
                    }
                }
                ret.addPartsGroup(partsGroup);
            }
            return ret;
        }
    }


    /*
     * パーツインデックスを保持するクラス。
     * パーツにはパーツIDとモーションから設定するパーツパラメータIDがある。
     * 文字列で設定することもできるが、インデックスを取得してから設定したほうが高速。
     */
    public class L2DPartsParam
    {
        public const int TYPE_VISIBLE = 0;
        public const bool optimize = false;
        public string id;
        public int paramIndex = -1;
        public int partsIndex = -1;
        public int type = TYPE_VISIBLE;

        public List<L2DPartsParam> link = null;// 連動するパーツ


        public L2DPartsParam(string id)
        {
            this.id = id;
        }


        /*
         * パラメータとパーツのインデックスを初期化する。
         * @param model
         */
        public void initIndex(ALive2DModel model)
        {
            if (type == TYPE_VISIBLE)
            {
                paramIndex = model.getParamIndex("VISIBLE:" + id);// パーツ表示のパラメータはVISIBLE:がつく。Live2Dアニメータの仕様。
            }
            partsIndex = model.getPartsDataIndex(PartsDataID.getID(id));
            model.setParamFloat(paramIndex, 1);
            //Log.d("live2d",id+ " param:"+paramIndex+" parts:"+partsIndex);
        }
    }
}