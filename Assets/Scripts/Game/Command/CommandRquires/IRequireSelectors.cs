using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.Card;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public interface IRequireSelectors : ICommandRequire<(ISelector rat,ISelector cat)>{}
}