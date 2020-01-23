using FundooCommonLayer.Model;
using FundooCommonLayer.ModelRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Interfaces
{
  public interface ILabelRepository
  {
    LabelModel AddLabels(string labels, int userid);
    LabelModel UpdateLabels(RequestedLabel requestedLabel, int userid,int labelid);
    bool DeleteLabel(int labelid);
  }
}
