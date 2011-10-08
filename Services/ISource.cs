using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyttaIn.Services
{
    public interface ISource
    {
        void Get(float latitude, float longitude);
    }
}