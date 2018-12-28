using System;
using System.Collections.Generic;

using System.Text;

namespace QzoneSDK.OAuth.Common
{
    public interface IErrorResponse
    {
        bool HasError { get; }
        string GetError();
    }
}
