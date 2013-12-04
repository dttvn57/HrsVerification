using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HrsVerification.Models
{
    [Table("HR_HW_TimeVerificationForm")]
    public class TimeVerificationForm   
    {
        public int TimeVerificationFormId   { get; set; }

        [StringLength(50)]
        public string  EmpFullName          { get; set; }

        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "Employee Id is required")]
        [StringLength(6)]
        public string   EmpIdNum            { get; set; }

        public string  Title                { get; set; }

        public  string  HomeBranchOrgId     { get; set; }

        public  EnumEmpStatus       EmpStatus   { get; set; }

        public  List<RouteTo>       RouteTos    { get; set; }

        private ICollection<WorkedTime>         _WorkedTimes;
        public virtual ICollection<WorkedTime> WorkedTimes
        {
            get { return _WorkedTimes ?? (_WorkedTimes = new HashSet<WorkedTime>()); }
            set { _WorkedTimes = value; }
        }
    }

    [TypeConverter(typeof(PascalCaseWordSplittingEnumConverter))]
    public enum EnumEmpStatus
    {
        SAN,
        Permanent,
        Temporary
    }
    public class EmpStatusViewModel
    {
        [Required(ErrorMessage = "Employment Status:")]
        public EnumEmpStatus EmpStatus { get; set; }
        //public EmpType EmpType
        //{
        //    get;
        //    set;
        //}

        public IEnumerable<SelectListItem> EmpTypes
        {
            get
            {
                //Enum.GetValues(typeof(Suit)).Select(x=>x).Where(x=> x != param)){}
                //return Enum.GetValues(typeof(EnumEmpStatus)).Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() });

                IEnumerable<EnumEmpStatus> values =
                                        Enum.GetValues(typeof(EnumEmpStatus))
                                        .Cast<EnumEmpStatus>();

                IEnumerable<SelectListItem> items =
                                        from value in values
                                        select new SelectListItem
                                        {
                                            Text = value.ToString(),
                                            Value = value.ToString()
                                            //Selected = value == selectedMovie,
                                        };
                return items;

//                return Enum.GetNames<EmpType>().Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() });
            }
        }
    }

    [Table("HR_HW_RouteTo")]
    public class RouteTo
    {
        public int      RouteToId           { get; set; }
        public string   SignedByEmpId       { get; set; }
        public DateTime SignDate            { get; set; }
        public int      Position            { get; set; }
    }

    [Table("HR_HW_Branch")]
    public class Branch
    {
        public int BranchId { get; set; }
        public string OrgId { get; set; }
        public string Name  { get; set; }
        public string FullName()
        {
            if (OrgId == null || (OrgId != null && OrgId.Count() == 0))
                return Name;
            else
                return Name + " \t(" + OrgId + ")";
        }
    }


    public class PascalCaseWordSplittingEnumConverter : EnumConverter
    {

        public PascalCaseWordSplittingEnumConverter(Type type)

            : base(type)
        {

        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string))
            {

                string stringValue = (string)base.ConvertTo(context, culture, value, destinationType);

                stringValue = SplitString(stringValue);

                return stringValue;

            }

            return base.ConvertTo(context, culture, value, destinationType);

        }

        public string SplitString(string stringValue)
        {

            StringBuilder buf = new StringBuilder(stringValue);

            // assume first letter is upper!

            bool lastWasUpper = true;

            int lastSpaceIndex = -1;

            for (int i = 1; i < buf.Length; i++)
            {

                bool isUpper = char.IsUpper(buf[i]);

                if (isUpper & !lastWasUpper)
                {

                    buf.Insert(i, ' ');

                    lastSpaceIndex = i;

                }

                if (!isUpper && lastWasUpper)
                {

                    if (lastSpaceIndex != i - 2)
                    {

                        buf.Insert(i - 1, ' ');

                        lastSpaceIndex = i - 1;

                    }

                }

                lastWasUpper = isUpper;

            }

            return buf.ToString();

        }

    }


}