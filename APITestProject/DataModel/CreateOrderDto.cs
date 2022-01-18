namespace APITestProject.DataModel
{
    public class CreateOrderDto
    {
        public string orderRef { get; set; }
        public Instruction[] instructions { get; set; }
        public Metadata metadata { get; set; }
        public Customerdefaults customerDefaults { get; set; }
    }

    public class Metadata
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

    public class Customerdefaults
    {
        public Customerref1 customerRef1 { get; set; }
        public Customerref2 customerRef2 { get; set; }
        public Customerref3 customerRef3 { get; set; }
    }

    public class Customerref1
    {
        public string financialInstrumentId { get; set; }
        public string schemeId { get; set; }
        public Metadata1 metadata { get; set; }
    }

    public class Metadata1
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

    public class Customerref2
    {
        public string financialInstrumentId { get; set; }
        public string schemeId { get; set; }
        public Metadata2 metadata { get; set; }
    }

    public class Metadata2
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

    public class Customerref3
    {
        public string financialInstrumentId { get; set; }
        public string schemeId { get; set; }
        public Metadata3 metadata { get; set; }
    }

    public class Metadata3
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

    public class Instruction
    {
        public string instructionRef { get; set; }
        public string customerRef { get; set; }
        public string direction { get; set; }
        public string financialInstrumentId { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
        public string settledByDate { get; set; }
        public string schemeId { get; set; }
        public Metadata4 metadata { get; set; }
    }

    public class Metadata4
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

}
