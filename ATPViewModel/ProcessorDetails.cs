using System.Collections.Generic;
using System.Windows.Data;

namespace ATPViewModel
{
    public class ProcessorDetails
    {
        public ProcessorDetails(string code, params FileDetailViewModel[] fileDetails)
        {
            Code = code;
            FileDetails = new ListCollectionView(fileDetails);
            if(fileDetails.Length>0)
                FileDetails.MoveCurrentToFirst();
        }

        public string Code { get; }
        public ListCollectionView FileDetails { get; }
    }
}
