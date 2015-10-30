using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Plugins;

/// <summary>
/// Class Plugin
/// 所有插件的父类，所有的在脚本中调用的函数都将使用
/// 插件实现
/// </summary>
public abstract class Plugin
{
    public static List<Plugin> plugins = null;

    public static bool execute(string name, string[] parameters)
    {
        for (int i = 0; i < plugins.Count; i++ )
        {
            Plugin plugin = plugins[i];
            if(plugin.matches(name))
            {
                plugin.execute(parameters);
                return true; 
            }
        }

        return false;
    }

    public static void init()
    {
        if(plugins == null)
        {
            plugins = new List<Plugin>();
            //plugins.Add(new ImagePlugin());
        }
    }


    abstract public bool matches(string name);

    abstract public void execute(string[] parameters);

}
