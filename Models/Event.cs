using System;
using System.ComponentModel.DataAnnotations;
namespace RestfulApiVisualCode.Models
{
    public class Event
    {
        public int Id { get; set; }    

        [Required(ErrorMessage ="������ ���� ������� ����")]
        public string Dateofevent{get;set;}

        [Required(ErrorMessage = "������ ���� ������ ����� ���")]
        public int Nameofasb { get; set; }

        [Required(ErrorMessage = "������ ���� ������� �������� ����������")]

        public string Nameofdevice { get; set; }

        [Required(ErrorMessage = "���������� ������� ������� �����������")]

        public string Isserios{get;set;}

        [Required(ErrorMessage = "���������� ������� ������������")]
        public string Discribeevent{get;set;}
        public string Fixevent{get;set;}
    }
}