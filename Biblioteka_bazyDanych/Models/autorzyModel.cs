namespace Biblioteka_bazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class autorzy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public autorzy()
        {
            this.ksiazki = new HashSet<ksiazki>();
        }

        [Display(Name = "ID")]
        public int id_autora { get; set; }
        [Display(Name = "Imię")]
        public string imie { get; set; }
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }
        [Display(Name = "Narodowość")]
        public string narodowosc { get; set; }
        [Display(Name = "Liczba dzieł")]
        public Nullable<int> liczba_dziel { get; set; }
        [Display(Name = "Życiorys")]
        public string zyciorys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ksiazki> ksiazki { get; set; }
    }
}