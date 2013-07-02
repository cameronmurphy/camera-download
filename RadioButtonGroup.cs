using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Camurphy.CameraDownload
{
    /// <summary>
    /// Allows a message of type object to be passed when an event is fired
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(object aMessage)
        {
            Message = aMessage;
        }

        public object Message { get; set; }
    }

    /// <summary>
    /// Provides one event to subscribe to for a group of radio buttons
    /// </summary>
    public class RadioButtonGroup
    {
        private List<RadioButton> _radioButtons;
        public event EventHandler CheckedChanged;

        public RadioButtonGroup()
        {
            _radioButtons = new List<RadioButton>();
        }

        public int SelectedIndex
        {
            get
            {
                foreach (RadioButton rb in _radioButtons)
                {
                    if (rb.Checked)
                        return _radioButtons.IndexOf(rb);
                }

                return -1;
            }

            set
            {
                (_radioButtons[value] as RadioButton).Checked = true;
            }
        }

        public void Add(RadioButton aItem)
        {
            aItem.CheckedChanged += new EventHandler(CheckedChangedHandler);
            _radioButtons.Add(aItem);
        }

        private void CheckedChangedHandler(object sender, EventArgs e)
        {
            EventHandler tempEH = CheckedChanged;
            tempEH(this, new EventArgs());
        }
    }
}
