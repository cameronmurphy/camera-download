using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Camurphy.CameraDownload
{
    class CustomComboBox : ComboBox
    {
        public int IndexOf(string aItemName)
        {
            for (int i = 0; i < this.Items.Count; i++)
                if (this.Items[i].ToString() == aItemName)
                    return i;

            return -1;
        }

        public bool Contains(string aItemName)
        {
            foreach (object o in this.Items)
                if (o.ToString() == aItemName)
                    return true;

            return false;
        }
    }
}
