﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BrawlLib.SSBBTypes;
using System.Linq;
using System.Windows.Forms;
using BrawlLib.Imaging;

namespace BrawlLib.SSBB.ResourceNodes
{
    public unsafe class TBRMNode : ARCEntryNode, IAttributeList
    {
        public override ResourceType ResourceType { get { return ResourceType.TBRM; } }
        internal TBRM* Header { get { return (TBRM*)WorkingUncompressed.Address; } }
        internal int unk0, unk1, unk2;

        // Internal buffer for editing - changes written back to WorkingUncompressed on rebuild
        internal UnsafeBuffer entries;

        [Category("TBRM")]
        public int NumEntries { get { return entries.Length / 4; } }
        [Category("TBRM")]
        public int Unk0 { get { return unk0; } set { unk0 = value; SignalPropertyChange(); } }
        [Category("TBRM")]
        public int Unk1 { get { return unk1; } set { unk1 = value; SignalPropertyChange(); } }
        [Category("TBRM")]
        public int Unk2 { get { return unk2; } set { unk2 = value; SignalPropertyChange(); } }

        public TBRMNode() { }

        public TBRMNode(VoidPtr address, int numEntries)
        {
            unk0 = 3;               // Default set in Hanenbow
            unk1 = 0;
            unk2 = 0;
            entries = new UnsafeBuffer((numEntries * 4));
            if (address == null)
            {
                byte* pOut = (byte*)entries.Address;
                for (int i = 0; i < (numEntries * 4); i++)
                    *pOut++ = 0;
            }
            else
            {
                byte* pIn = (byte*)address;
                byte* pOut = (byte*)entries.Address;
                for (int i = 0; i < (numEntries * 4); i++)
                    *pOut++ = *pIn++;
            }
        }
        ~TBRMNode() { entries.Dispose(); }


        public override bool OnInitialize()
        {
            unk0 = Header->_unk0;
            unk1 = Header->_unk1;
            unk2 = Header->_unk2;
            entries = new UnsafeBuffer(WorkingUncompressed.Length - 0x10);
            Memory.Move(entries.Address, Header->Entries, (uint)entries.Length);
            return false;
        }

        protected override string GetName()
        {
            return base.GetName("TBRM");
        }

        public override void OnRebuild(VoidPtr address, int length, bool force)
        {
            TBRM* header = (TBRM*)address;
            header->_tag = TBRM.Tag;
            header->_unk0 = unk0;
            header->_unk1 = unk1;
            header->_unk2 = unk2;
            Memory.Move(header->Entries, entries.Address, (uint)entries.Length);
        }

        public override int OnCalculateSize(bool force)
        {
            return 0x10 + entries.Length;
        }

        internal static ResourceNode TryParse(DataSource source) { return ((TBRM*)source.Address)->_tag == TBRM.Tag ? new TBRMNode() : null; }
        [Browsable(false)]
        public VoidPtr AttributeAddress
        {
            get
            {
                return entries.Address;
            }
        }
        public void SetFloat(int index, float value)
        {
            if (((bfloat*)AttributeAddress)[index] != value)
            {
                ((bfloat*)AttributeAddress)[index] = value;
                SignalPropertyChange();
            }
        }
        public float GetFloat(int index)
        {
            return ((bfloat*)AttributeAddress)[index];
        }
        public void SetInt(int index, int value)
        {
            if (((bint*)AttributeAddress)[index] != value)
            {
                ((bint*)AttributeAddress)[index] = value;
                SignalPropertyChange();
            }
        }
        public int GetInt(int index)
        {
            return ((bint*)AttributeAddress)[index];
        }

        public void SetRGBAPixel(int index, string value)
        {
            RGBAPixel p = new RGBAPixel();

            string s = value.ToString();
            char[] delims = new char[] { ',', 'R', 'G', 'B', 'A', ':', ' ' };
            string[] arr = s.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            if (arr.Length == 4)
            {
                byte.TryParse(arr[0], out p.R);
                byte.TryParse(arr[1], out p.G);
                byte.TryParse(arr[2], out p.B);
                byte.TryParse(arr[3], out p.A);
            }

            if (((RGBAPixel*)AttributeAddress)[index] != p)
            {
                ((RGBAPixel*)AttributeAddress)[index] = p;
                SignalPropertyChange();
            }
        }

        public RGBAPixel GetRGBAPixel(int index)
        {
            return ((RGBAPixel*)AttributeAddress)[index];
        }

        public void SetHex(int index, string value)
        {
            string field0 = (value.ToString() ?? "").Split(' ')[0];
            int fromBase = field0.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) ? 16 : 10;
            int temp = Convert.ToInt32(field0, fromBase);
            if (((bint*)AttributeAddress)[index] != temp)
            {
                ((bint*)AttributeAddress)[index] = temp;
                SignalPropertyChange();
            }
        }

        public String GetHex(int index)
        {
            return "0x" + ((int)((bint*)AttributeAddress)[index]).ToString("X8");
        }

        public IEnumerable<AttributeInterpretation> GetPossibleInterpretations()
        {
            ReadConfig();
            ResourceNode root = this;
            while (root.Parent != null) root = root.Parent;
            var q = from f in TBRMFormats
                    where 0x14 + f.NumEntries * 4 == WorkingUncompressed.Length
                    select f;

            bool any_match_name = q.Any(f => String.Equals(
                Path.GetFileNameWithoutExtension(f.Filename),
                root.Name.Replace("STG", "") + "[" + FileIndex + "]",
                StringComparison.InvariantCultureIgnoreCase));
            if (!any_match_name) q = q.Concat(new AttributeInterpretation[] { GenerateDefaultInterpretation() });

            q = q.OrderBy(f => !String.Equals(
                Path.GetFileNameWithoutExtension(f.Filename),
                root.Name.Replace("STG", "") + "[" + FileIndex + "]",
                StringComparison.InvariantCultureIgnoreCase));

            return q;
        }

        private AttributeInterpretation GenerateDefaultInterpretation()
        {
            AttributeInfo[] arr = new AttributeInfo[NumEntries];
            buint* pIn = (buint*)AttributeAddress;
            int index = 0x10;

            ResourceNode root = this;
            while (root.Parent != null) root = root.Parent;

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new AttributeInfo()
                {
                    _name = "0x" + index.ToString("X3")
                };
                //Guess if the value is a an integer or float
                uint u = (uint)*((buint*)pIn);
                float f = (float)*((bfloat*)pIn);
                RGBAPixel p = new RGBAPixel(u);
                if (*pIn == 0)
                {
                    arr[i]._type = 0;
                    arr[i]._description = "Default: 0 (Could be int or float - be careful)";
                }
                else if ((((u >> 24) & 0xFF) != 0 && *((bint*)pIn) != -1 && !float.IsNaN(f)) || (p.R == 0 && p.G == 50 && p.B == 0))
                {
                    float abs = Math.Abs(f);
                    if ((abs > 0.0000001 && abs < 10000000) || float.IsInfinity(abs))
                    {
                        arr[i]._type = 0;
                        arr[i]._description = "Default (Float): " + f + " (0x" + u.ToString("X8") + ")";
                    }
                    else if ((p.R % 5 == 0 || p.R % 3 == 0) && (p.B % 5 == 0 || p.B % 3 == 0) &&
                             (p.G % 5 == 0 || p.G % 3 == 0) && (p.A == 0 || p.A == 255))
                    {
                        arr[i]._type = 3;
                        arr[i]._description = "Default (Color): " + p + " (0x" + u.ToString("X8") + ")";
                        arr[i]._name = arr[i]._name;
                    }
                    else
                    {
                        arr[i]._type = 4;
                        arr[i]._description = "Default (Unknown Type): " + "(0x" + u.ToString("X8") + ")";
                        arr[i]._name = "~" + arr[i]._name;
                    }
                }
                else
                {
                    arr[i]._type = 1;
                    arr[i]._description = "Default (Integer): " + u + " (0x" + u.ToString("X8") + ")";
                    arr[i]._name = "*" + arr[i]._name;
                }
                index += 4;
                pIn++;
            }

            string temp = "";
            if (root != this)
                temp = "[" + FileIndex + "]";
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\StageDocumentation" + "\\TBRM\\" + root.Name.Replace("STG", "") + temp + ".txt";
            return new AttributeInterpretation(arr, filename);
        }

        private static List<AttributeInterpretation> TBRMFormats = new List<AttributeInterpretation>();
        private static HashSet<string> configpaths_read = new HashSet<string>();

        private static void ReadConfig()
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\StageDocumentation"))
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\StageDocumentation" + "\\TBRM"))
                {
                    foreach (string path in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "\\StageDocumentation" + "\\TBRM", "*.txt"))
                    {
                        if (configpaths_read.Contains(path)) continue;
                        configpaths_read.Add(path);
                        try
                        {
                            TBRMFormats.Add(new AttributeInterpretation(path));
                        }
                        catch (FormatException ex)
                        {
                            if (Properties.Settings.Default.HideMDL0Errors)
                            {
                                Console.Error.WriteLine(ex.Message);
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
