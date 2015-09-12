using System;

namespace SwissKnife.Attributes
{
    /// <summary>
    /// This attribute is used to represent a StringValue for a value in an Enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class StringValueAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets the key of the StringValueAttribute
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// Gets the StringValue of the StringValueAttribute
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the StringValueAttribute class.
        /// </summary>
        /// <param name="stringValue">The StringValue of this attribute.</param>
        public StringValueAttribute(string stringValue)
            : this(stringValue, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the StringValueAttribute class.
        /// </summary>
        /// <param name="stringValue">The StringValue of this attribute.</param>
        /// <param name="key">The key of this attribute.</param>
        public StringValueAttribute(string stringValue, string key)
        {
            this.StringValue = stringValue;
            this.Key = key;
        }

        #endregion
    }
}
