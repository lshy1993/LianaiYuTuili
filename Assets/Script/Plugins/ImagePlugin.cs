using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Plugins
{
//    class ImagePlugin : Plugin
//    {
////        private string name;
////        private ImageManager imgManager= GameManager.instance.imgManager; 
//        public ImagePlugin()
//        {


//        }

//        public override bool matches(string name)
//        {
//            //throw new NotImplementedException();
//            if(name.Equals("setImg")||
//                name.Equals("setImgEx")||
//                name.Equals("fadeIn")||
//                name.Equals("fadeOut"))
//            {
//                this.name = name;
//                return true;
//            }
                
//            return false;
//        }

//        public override void execute(string[] parameters)
//        {
//            if(name.Equals("setImg"))
//            {
//                if(parameters.Length == 1)
//                {
//                    imgManager.setImage(parameters[0], ImageManager.MIDDLE(), 0);
//                }
//                else if(parameters.Length == 2)
//                {
//                    if(parameters[1].Equals("L")) 
//                        imgManager.setImage(parameters[0], ImageManager.LEFT(), 0);
//                    else if(parameters[1].Equals("M"))
//                        imgManager.setImage(parameters[0], ImageManager.MIDDLE(), 0);
//                    else if(parameters[1].Equals("R"))
//                        imgManager.setImage(parameters[0], ImageManager.RIGHT(), 0);
//                }

//            }
//            else if(name.Equals("setImgEx"))
//            {
//                string img = parameters[0];
//                Vector3 position = new Vector3(int.Parse(parameters[1]),int.Parse(parameters[2], 0));
//                int layer = int.Parse(parameters[3]);
//                float zoom = float.Parse(parameters[4]);
//                float alpha = float.Parse(parameters[5]);
//                imgManager.setImage(img, position, layer, zoom, alpha);
//            }
//            else if(name.Equals("fadeIn"))
//            {

//            }
//            else if(name.Equals("fadeOut"))
//            {

//            }
//        }
//    }
}
