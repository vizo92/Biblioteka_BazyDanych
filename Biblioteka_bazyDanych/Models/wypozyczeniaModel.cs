namespace Biblioteka_bazyDanych
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class wypozyczenia
    {
        [Display(Name = "ID")]
        public int id_wypozyczenia { get; set; }
        [Display(Name = "Czytelnik")]
        public int id_czytelnika { get; set; }
        [Display(Name = "Książka")]
        public int id_ksiazki { get; set; }
        [Display(Name = "Data zamówienia")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime data_zamowienia { get; set; }
        [Display(Name = "Data wypożyczenia")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<DateTime> data_wypozyczenia { get; set; }
        [Display(Name = "Data zwrotu")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<DateTime> data_zwrotu { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        public virtual czytelnicy czytelnicy { get; set; }
        public virtual ksiazki ksiazki { get; set; }

    }
}
