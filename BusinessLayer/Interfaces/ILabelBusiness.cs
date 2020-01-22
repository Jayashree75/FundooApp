namespace FundooBusinessLayer.Interfaces
{
    using FundooCommonLayer.Model;
    using System;
  using System.Collections.Generic;
  using System.Text;


  public interface ILabelBusiness
  {
    LabelModel AddLabels(string labels, long userid);
    LabelModel UpdateLabels(string labels, long userid);
  }
}
