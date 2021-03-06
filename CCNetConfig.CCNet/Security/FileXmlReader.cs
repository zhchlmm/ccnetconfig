﻿using System;
using System.Collections.Generic;
using System.Text;
using CCNetConfig.Core;
using CCNetConfig.Core.Components;
using System.ComponentModel;
using System.Xml;

namespace CCNetConfig.CCNet.Security
{
    /// <summary>
    /// An XML-based file reader.
    /// </summary>
    [MinimumVersion("1.5")]
    [ReflectorName("xmlFileAuditReader")]
    [DisplayName("XML File Logger")]
    public class FileXmlReader
        : SecurityAuditReader
    {
        #region Private fields
        private string location;
        #endregion

        #region Constructors
        /// <summary>
        /// Start a new instance of a <see cref="FileXmlReader"/>.
        /// </summary>
        public FileXmlReader()
        {
            Type = "FileXmlReader";
        }
        #endregion

        #region Public properties
        #region Location
        /// <summary>
        /// The location of the log file.
        /// </summary>
        [Description("The location of the log file.")]
        [DisplayName("Log File Location")]
        [Category("Optional")]
        [DefaultValue(null)]
        [Required(MinimumVersion = "1.5")]
        public virtual string Location
        {
            get { return location; }
            set
            {
                location = value;
                FirePropertyChanged("Location");
            }
        }
        #endregion
        #endregion

        #region Public methods
        #region Serialize()
        /// <summary>
        /// Serialises the security setting to an <see cref="XmlElement"/>.
        /// </summary>
        /// <returns>The <see cref="XmlElement"/> containing the security setting configuration.</returns>
        public override XmlElement Serialize()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("auditReader");
            root.SetAttribute("type", "xmlFileAuditReader");
            if (!string.IsNullOrEmpty(Location)) root.SetAttribute("location", Location);
            return root;
        }
        #endregion

        #region Deserialize()
        /// <summary>
        /// Deserialises an <see cref="XmlElement"/> containing security setting configuration.
        /// </summary>
        /// <param name="element">The <see cref="XmlElement"/> to deserialise.</param>
        public override void Deserialize(XmlElement element)
        {
            Type = "FileXmlReader";
            Location = Util.GetElementOrAttributeValue("location", element);
        }
        #endregion

        #region Clone()
        /// <summary>
        /// Generates a clone of this security setting configuration.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            FileXmlReader newValue = new FileXmlReader();
            CopyTo(newValue);
            newValue.Location = Location;
            return newValue;
        }
        #endregion

        #region ToString()
        /// <summary>
        /// Displays a friendly message for the item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} (XML file)",
                string.IsNullOrEmpty(Location) ? string.Empty : Location).Trim();
        }
        #endregion
        #endregion

        #region Protected methods
        #region CopyTo()
        /// <summary>
        /// Copies the settings to another instance of this class.
        /// </summary>
        /// <param name="value">The target instance.</param>
        protected virtual void CopyTo(FileXmlReader value)
        {
            base.CopyTo(value);
            value.Location = Location;
        }
        #endregion
        #endregion
    }
}
