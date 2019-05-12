namespace Biblioteka_bazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class czytelnicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public czytelnicy()
        {
            this.wypozyczenia = new HashSet<wypozyczenia>();
        }

        [Display(Name = "ID")]
        public int id_czytelnika { get; set; }
        [Display(Name = "Imię")]
        public string imie { get; set; }
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }
        [Display(Name = "Miasto")]
        public string miastso { get; set; }
        [Display(Name = "Ulica")]
        public string ulica { get; set; }
        [Display(Name = "Liczba książek")]
        public Nullable<int> liczba_ksiazek { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wypozyczenia> wypozyczenia { get; set; }
    }
}
