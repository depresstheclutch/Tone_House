using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToneHouseMvc.Models
{
    public class DietContentFetch
    {
        //Weight gain category----------------------
        public List<tb_Diet> wgbreakfast { get; set; }
        public List<tb_Diet> wglunch { get; set; }
        public List<tb_Diet> wgsnacks { get; set; }

        public List<tb_Diet> wgdinner { get; set; }

        //Weight loss category-----------------------

        public List<tb_Diet> wlbreakfast { get; set; }
        public List<tb_Diet> wllunch { get; set; }
        public List<tb_Diet> wlsnacks { get; set; }

        public List<tb_Diet> wldinner { get; set; }

        public DietContentFetch()
        {
            // weight gain
            this.wgbreakfast = new List<tb_Diet>();
            this.wglunch = new List<tb_Diet>();
            this.wgsnacks = new List<tb_Diet>();
            this.wgdinner = new List<tb_Diet>();

            //weight loss
            this.wlbreakfast = new List<tb_Diet>();
            this.wllunch = new List<tb_Diet>();
            this.wlsnacks = new List<tb_Diet>();
            this.wldinner = new List<tb_Diet>();
        }
    }
}