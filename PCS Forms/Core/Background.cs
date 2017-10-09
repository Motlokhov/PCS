

namespace PCS_Forms.Core
{
    
    public class Background
    {
       public Education Education { get; private set; }
       public Composition_of_family Family { get; private set; }
       public Detained Detained { get; private set; }
       public Defect Defect { get; private set; }
       public Suicide_in_family Suicide { get; private set; }

        public Background(Education education, Composition_of_family family, Detained detained, Defect defect, Suicide_in_family suicide)
        {
            this.Education = education;
            this.Family = family;
            this.Detained = detained;
            this.Defect = defect;
            this.Suicide = suicide;
        }
    }
}
