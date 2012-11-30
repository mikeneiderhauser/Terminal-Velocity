using System.Collections.Generic;

namespace Interfaces
{
    public interface IStatus
    {
        /// <summary>
        ///     Property to operate on _trains
        /// </summary>
        List<ITrainModel> Trains { get; set; }

        /// <summary>
        ///     Property to operate on _blocks
        /// </summary>
        List<IBlock> Blocks { get; set; }
    }
}