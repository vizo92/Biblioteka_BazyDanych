namespace Biblioteka_bazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class wydawnictwa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wydawnictwa()
        {
            this.ksiazki = new HashSet<ksiazki>();
        }

        [Display(Name = "Nazwa")]
        public string nazwa { get; set; }
        [Display(Name = "Kraj")]
        public string kraj { get; set; }
        [Display(Name = "Miasto")]
        public string miasto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ksiazki> ksiazki { get; set; }
    }
}
