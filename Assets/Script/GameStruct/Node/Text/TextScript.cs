﻿using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public abstract class TextScript : GameNode
    {
        private int current
        {
            set { manager.gameData.currentTextPos = value; }
            get { return manager.gameData.currentTextPos; }
        }

        public IList<Piece> pieces;
        protected PieceFactory f;
        protected NodeFactory nodeFactory;

        public TextScript(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps)
        {
        }

        public override void Init()
        {
            base.Init();
            pieces = null;
            f = new PieceFactory(root, manager);
            nodeFactory = NodeFactory.GetInstance();
            InitText();
            ps.SwitchTo_VerifyIterative("DialogBox_Panel", Update);
        }

        public abstract void InitText();

        public override void Update()
        {
            if (manager.isEffecting)
            {
                //Debug.Log("Effects Running Throw Click Event!");
                return;
            }
            if (pieces != null && current >= 0 && current < pieces.Count)
            {
                if (pieces[current].GetType() == typeof(EffectPiece))
                {
                    //图像效果处理块
                    manager.isEffecting = true;
                    EffectPiece ep = (EffectPiece)pieces[current];
                    ep.ExecAuto(new Action(() => { manager.isEffecting = false; current = ep.Next(); Update(); }));
                }
                else if ( pieces[current].GetType() == typeof(SoundPiece))
                {
                    //声音处理块
                    manager.isEffecting = true;
                    SoundPiece sp = (SoundPiece)pieces[current];
                    sp.ExecAuto(new Action(() => { manager.isEffecting = false; current = sp.Next(); Update(); }));
                }
                else if (pieces[current].GetType() == typeof(ChapterNamePiece))
                {
                    //章节名显示模块
                    ChapterNamePiece cnp = (ChapterNamePiece)pieces[current];
                    cnp.Exec();
                    current = cnp.Next();
                    Update();
                }
                else if( pieces[current].GetType() == typeof(EviGetPiece))
                {
                    //证据显示处理块
                    manager.BlockRightClick();
                    EviGetPiece ev = (EviGetPiece)pieces[current];
                    ev.Exec();
                    if (ev.finished)
                    {
                        current = ev.Next();
                        manager.UnblockRightClick();
                        Update();
                    }
                }
                else if (pieces[current].GetType() == typeof(HPPiece))
                {
                    //扣血模块
                    manager.BlockRightClick();
                    HPPiece hp = (HPPiece)pieces[current];
                    hp.Exec();
                    if (hp.finished)
                    {
                        current = hp.Next();
                        manager.UnblockRightClick();
                        Update();
                    }
                }
                else if (pieces[current].GetType() == typeof(DiaboxPiece))
                {
                    //对话框控制模块
                    manager.isEffecting = true;
                    manager.BlockRightClick();
                    DiaboxPiece dp = (DiaboxPiece)pieces[current];
                    dp.ExecAuto(new Action(() => { manager.isEffecting = false; manager.UnblockRightClick(); Update(); }));
                    current = dp.Next();
                }
                else if (pieces[current].GetType() == typeof(InputPiece))
                {
                    //姓名输入模块
                    manager.isEffecting = true;
                    manager.BlockRightClick();
                    InputPiece ip = (InputPiece)pieces[current];
                    ip.ExecAuto(new Action(() => { manager.isEffecting = false; manager.UnblockRightClick(); Update(); }));
                    current = ip.Next();
                }
                else if (pieces[current].GetType() == typeof(TimeSwitchPiece))
                {
                    //地点转换模块
                    manager.BlockRightClick();
                    TimeSwitchPiece tsp= (TimeSwitchPiece)pieces[current];
                    if (tsp.finished)
                    {
                        tsp.ExecAuto(() => {
                            manager.UnblockRightClick();
                            Update();
                        });
                        current = tsp.Next();
                    }
                    else
                    {
                        tsp.Exec();
                    }
                }
                else
                {
                    //文字块需等待点击
                    TextPiece t = (TextPiece)pieces[current];
                    if (t.finish)
                    {
                        t.Clear();
                        current = t.Next();
                        Update();
                    }
                    else
                    {
                        //Debug.Log("文字块启用");
                        t.Exec();
                    }
                }
            }
            else
            {
                end = true;
            }

        }


        public override string ToString()
        {
            string str = "";
            foreach (Piece p in pieces)
            {
                TextPiece tp = p as TextPiece;
                if (tp != null)
                {
                    str += tp.ToString();
                }
            }
            return base.ToString() + str;
        }

    }
}
