using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JointTrackerManager
{
    class JointConfigure
    {
        public enum BoneKind { Fix, Movable };

        public BoneKind IsMovableBone { get; set; }
        public string JointRootSerial { get; set; }
        public byte WorkerAddress { get; set; }
        public byte TrackerIndex { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float QuaternionX { get; set; }
        public float QuaternionY { get; set; }
        public float QuaternionZ { get; set; }
        public float QuaternionW { get; set; }

        static public DataGridViewColumn[] GetColumns()
        {
            DataGridViewComboBoxColumn isMovable = new DataGridViewComboBoxColumn();
            isMovable.Items.Add(BoneKind.Fix);
            isMovable.Items.Add(BoneKind.Movable);
            isMovable.DataPropertyName = "IsMovableBone";
            isMovable.HeaderText = "ボーンの\n種類";

            DataGridViewTextBoxColumn serial = new DataGridViewTextBoxColumn();
            serial.DataPropertyName = "JointRootSerial";
            serial.HeaderText = "ジョイント先\nシリアルナンバー";
            serial.Width = 120;

            DataGridViewTextBoxColumn slave = new DataGridViewTextBoxColumn();
            slave.DataPropertyName = "WorkerAddress";
            slave.HeaderText = "Worker\nAddress";

            DataGridViewTextBoxColumn tracker = new DataGridViewTextBoxColumn();
            tracker.DataPropertyName = "TrackerIndex";
            tracker.HeaderText = "Tracker\nIndex";

            DataGridViewTextBoxColumn position = new DataGridViewTextBoxColumn();
            position.HeaderText = "Bone\nLength";
            position.Width = 70;
            position.ReadOnly = true;

            DataGridViewTextBoxColumn px = new DataGridViewTextBoxColumn();
            px.DataPropertyName = "PositionX";
            px.HeaderText = "X";
            px.Width = 40;
            DataGridViewTextBoxColumn py = new DataGridViewTextBoxColumn();
            py.DataPropertyName = "PositionY";
            py.HeaderText = "Y";
            py.Width = 40;
            DataGridViewTextBoxColumn pz = new DataGridViewTextBoxColumn();
            pz.DataPropertyName = "PositionZ";
            pz.HeaderText = "Z";
            pz.Width = 40;


            DataGridViewTextBoxColumn rotation = new DataGridViewTextBoxColumn();
            rotation.HeaderText = "Bone\nRotation";
            rotation.Width = 70;
            rotation.ReadOnly = true;

            DataGridViewTextBoxColumn qx = new DataGridViewTextBoxColumn();
            qx.DataPropertyName = "QuaternionX";
            qx.HeaderText = "X";
            qx.Width = 40;
            DataGridViewTextBoxColumn qy = new DataGridViewTextBoxColumn();
            qy.DataPropertyName = "QuaternionY";
            qy.HeaderText = "Y";
            qy.Width = 40;
            DataGridViewTextBoxColumn qz = new DataGridViewTextBoxColumn();
            qz.DataPropertyName = "QuaternionZ";
            qz.HeaderText = "Z";
            qz.Width = 40;
            DataGridViewTextBoxColumn qw = new DataGridViewTextBoxColumn();
            qw.DataPropertyName = "QuaternionW";
            qw.HeaderText = "W";
            qw.Width = 40;


            return new DataGridViewColumn[] {
                isMovable,
                serial, slave, tracker,
                position, px, py, pz,
                rotation, qx, qy, qz, qw
            };
        }

        public JointConfigure(string[] values)
        {
            if (values.Length != 11) throw new Exception("項目数が異なります");

            bool kind_b;
            int kind_i;
            if (bool.TryParse(values[0], out kind_b))
            {
                IsMovableBone = kind_b ? BoneKind.Movable : BoneKind.Fix;
            }
            else if (int.TryParse(values[0], out kind_i))
            {
                IsMovableBone = kind_i > 0 ? BoneKind.Movable : BoneKind.Fix;
            }
            else
            {
                throw new Exception("可動ボーンは 0, 1 または true, false で表さなければなりません");
            }

            if (values[1].Length == 0) new Exception("ジョイント元は空白にはできません");
            JointRootSerial = values[1];

            byte b;
            byte minWorkerAddress = (byte)((IsMovableBone == BoneKind.Movable) ? 1 : 0);
            if (byte.TryParse(values[2], out b))
            {
                if (b >= minWorkerAddress && b <= 19) WorkerAddress = b;
                else throw new Exception("WorkerAddress は " + minWorkerAddress + " - 19 の数値である必要があります");
            }
            else
            {
                throw new Exception("WorkerAddress は " + minWorkerAddress + " - 19 の数値である必要があります");
            }

            if (byte.TryParse(values[3], out b))
            {
                if (b >= 60) throw new Exception("Tracker Index は 0 - 60 の数値である必要があります。詳細はVirtual Motion Trackerの仕様を参照してください");
                TrackerIndex = b;
            }
            else
            {
                throw new Exception("Tracker Index は 0 - 60 の数値である必要があります。詳細はVirtual Motion Trackerの仕様を参照してください");
            }

            float f;
            if (!values.Where((_, i) => i > 3).All(x => float.TryParse(x, out f)))
            {
                throw new Exception("Position, Quaternionの成分は float である必要があります。");
            }
            float[] fs = values.Where((_, i) => i > 3).Select(x => float.Parse(x)).ToArray();

            PositionX = fs[0];
            PositionY = fs[1];
            PositionZ = fs[2];
            QuaternionX = fs[3];
            QuaternionY = fs[4];
            QuaternionZ = fs[5];
            QuaternionW = fs[6];
        }

        override public string ToString()
        {
            return string.Join(",", IsMovableBone == BoneKind.Movable ? true : false, JointRootSerial, WorkerAddress, TrackerIndex, PositionX, PositionY, PositionZ, QuaternionX, QuaternionY, QuaternionZ, QuaternionW);
        }
    }
}
