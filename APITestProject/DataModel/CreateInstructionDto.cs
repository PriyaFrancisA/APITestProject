namespace APITestProject.DataModel
{
    public class CreateInstructionDto
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
        public Metadata metadata { get; set; }
    }
 }

