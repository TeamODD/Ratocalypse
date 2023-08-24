using System;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public interface IRequireExtraExcutor : ICommandRequire<Func<CommandExecutor>>{}
}