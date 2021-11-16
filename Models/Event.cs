using System;
using System.ComponentModel.DataAnnotations;
namespace RestfulApiVisualCode.Models
{
    public class Event
    {
        public int Id { get; set; }    

        [Required(ErrorMessage ="Должна быть указана дата")]
        public string Dateofevent{get;set;}

        [Required(ErrorMessage = "Должен быть указан номер АСБ")]
        public int Nameofasb { get; set; }

        [Required(ErrorMessage = "Должно быть указано название устройства")]

        public string Nameofdevice { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать уровень серьезности")]

        public string Isserios{get;set;}

        [Required(ErrorMessage = "Необходимо описать происшествие")]
        public string Discribeevent{get;set;}
        public string Fixevent{get;set;}
    }
}