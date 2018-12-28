using System;
using System.Collections.Generic;

using System.Text;

namespace QzoneSDK.OAuth.Common.Exceptions
{
    [Serializable]
    public class OAuthException : Exception
    {
        public OAuthException(string message)
            : base(message)
        {
        }
    }
}
