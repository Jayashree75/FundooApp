using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Interfaces
{
  public interface ILabelRepository
  {
    LabelModel AddLabels(string labels, long userid);
    LabelModel UpdateLabels(string labels, long userid);
  }
}
