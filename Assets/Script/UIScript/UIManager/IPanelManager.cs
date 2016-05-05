using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IPanelManager
{
    IEnumerator Open();
    IEnumerator Close();
    
}
