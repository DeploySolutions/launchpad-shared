using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Deploy.LaunchPad.AWS.S3
{

    public partial class ReadOnlyS3FileCollection<TFile> : IReadOnlyS3FileCollection<TFile>, IEnumerable<TFile>
        where TFile : IS3FileInfo
    {
        private readonly List<TFile> _files;

        public ReadOnlyS3FileCollection()
        {
            _files = new List<TFile>();
        }

        public ReadOnlyS3FileCollection(IEnumerable<TFile> files)
        {
            _files = new List<TFile>(files);
        }

        public IEnumerable<TFile> Files => _files;

        public IEnumerator<TFile> GetEnumerator()
        {
            return _files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _files.GetEnumerator();
        }
    }
}