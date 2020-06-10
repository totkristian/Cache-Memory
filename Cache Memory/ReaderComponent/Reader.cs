using HistoricalComponent;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderComponent
{
    public class Reader
    {
        private Historical historical = Historical.GetInstance();
        private Codes code;

        public Codes Code { get => code; set => code = value; }


        public List<HistoricalProperty> GetChangesForInterval(Codes code)
        {
            switch (code)
            {
                case Codes.CODE_ANALOG:
                    return ReadCodeAnalog(code);

                case Codes.CODE_DIGITAL:
                    return ReadCodeDigital(code);

                case Codes.CODE_CONSUMER:
                    return ReadCodeConsumer(code);

                case Codes.CODE_CUSTOM:
                    return ReadCodeCustom(code);

                case Codes.CODE_LIMITSET:
                    return ReadCodeLimitset(code);

                case Codes.CODE_MOTION:
                    return ReadCodeMotion(code);

                case Codes.CODE_MULTIPLENODE:
                    return ReadCodeMultiplenode(code);

                case Codes.CODE_SENSOR:
                    return ReadCodeSensor(code);

                case Codes.CODE_SINGLENODE:
                    return ReadCodeSinglenode(code);

                case Codes.CODE_SOURCE:
                    return ReadCodeSource(code);

                default:
                    return null;
            }
        }

        private List<HistoricalProperty> ReadCodeSource(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeSinglenode(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeSensor(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeMultiplenode(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeMotion(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeLimitset(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeCustom(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeConsumer(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeDigital(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeAnalog(Codes code)
        {
            throw new NotImplementedException();
        }
    }
}
