using System.Collections.Generic;

namespace ApplicationMediator.Dto._4Digests
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompanyDetailsDto CompanyDetails { get; set; }
        public List<HouseDto> Houses { get; set; }
    }
}