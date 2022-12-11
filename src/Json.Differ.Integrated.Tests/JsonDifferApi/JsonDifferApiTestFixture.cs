using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Integrated.Tests.JsonDifferApi
{
    public class JsonDifferApiTestFixture
    {
        public JsonDifferApiTestFixture()
        {
            ExternalId = Guid.NewGuid();
        }

        public readonly Guid ExternalId;
    }
}
