using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class CharacterManager
    {
        private static CharacterManager instance;
        public static CharacterManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CharacterManager();
            }
            return instance;
        }

        public static readonly string PATH = "Text/Characters/";
        public static readonly string DEBUG = "Text/CharacterDebug/";

        public Dictionary<string, Character> characterTable;

        public Character GetCharacter(string name)
        {
            return characterTable[name];
        }

        public static Dictionary<string, Character> GetStaticCharacters()
        {
            string path = Constants.DEBUG ? DEBUG : PATH;
            Dictionary<string, Character> characters = new Dictionary<string, Character>();
            foreach(TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                Character character = new Character(text.text);
                characters.Add(character.name, character);
            }

            return characters;
        }
    }
}
