using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public interface IDisplay
    {
        void PrintConnectPhone();

        void PrintLoadRFID();
        void PrintConnectionFailure();
        void PrintCharging();
        void PrintRFIDFail();
        void PrintRemovePhone();
    }
}
