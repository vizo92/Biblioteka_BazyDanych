namespace Biblioteka_bazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ksiazki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ksiazki()
        {
            this.wypozyczenia = new HashSet<wypozyczenia>();
        }

        [Display(Name = "ID")]
        public int id_ksiazki { get; set; }
        [Display(Name = "Autor")]
        public int id_autora { get; set; }
        [Display(Name = "Wydawnictwo")]
        public string wydawnictwo { get; set; }
        [Display(Name = "Gatunek")]
        public string gatunek { get; set; }
        [Display(Name = "Tytuł")]
        public string tytul { get; set; }

        public virtual autorzy autorzy { get; set; }
        public virtual gatunki gatunki { get; set; }
        public virtual wydawnictwa wydawnictwa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wypozyczenia> wypozyczenia { get; set; }
    }
}
