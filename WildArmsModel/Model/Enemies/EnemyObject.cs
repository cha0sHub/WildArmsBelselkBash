using System.Collections.ObjectModel;
using System.Collections.Generic;
using DiscDataManipulation.Model;
using WildArms.Helper;

namespace WildArmsModel.Model.Enemies
{
    public class EnemyObject : DiscMappedObject
    {
        public const int ObjectSize = 0x47800;
        public const int DataLength = 0xC8 + 300;

        public List<int> AttackIds => new List<int>
            {
                Attack1Id,
                Attack2Id,
                Attack3Id,
                Attack4Id,
                Attack5Id,
                Attack6Id,
                Attack7Id,
                Attack8Id
            };
        public List<int> AttackProbabilities => new List<int>
        {
                Attack1Probability,
                Attack2Probability,
                Attack3Probability,
                Attack4Probability,
                Attack5Probability,
                Attack6Probability,
                Attack7Probability,
                Attack8Probability
        };

        public byte Level
        {
            get
            {
                return ReadByte(EnemyOffsets.Level);
            }
            set
            {
                if (Level == value)
                    return;
                WriteByte(value, EnemyOffsets.Level);
                NotifyPropertyChanged();
            }
        }

        public ushort Hp
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Hp1);
            }
            set
            {
                if (Hp == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Hp1);
                WriteUnsignedShort(value, EnemyOffsets.Hp2);
                NotifyPropertyChanged();
            }
        }

        public ushort Mp
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Mp1);
            }
            set
            {
                if (Mp == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Mp1);
                WriteUnsignedShort(value, EnemyOffsets.Mp2);
                NotifyPropertyChanged();
            }
        }
        public ushort Atp
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Atp);
            }
            set
            {
                if (Atp == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Atp);
                NotifyPropertyChanged();
            }
        }
        public ushort Sor
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Sor);
            }
            set
            {
                if (Sor == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Sor);
                NotifyPropertyChanged();
            }
        }
        public ushort Dfp
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Dfp);
            }
            set
            {
                if (Dfp == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Dfp);
                NotifyPropertyChanged();
            }
        }
        public ushort Mgr
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Mgr);
            }
            set
            {
                if (Mgr == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Mgr);
                NotifyPropertyChanged();
            }
        }

        public ushort Res
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Res);
            }
            set
            {
                if (Res == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Res);
                NotifyPropertyChanged();
            }
        }
        public ushort Pry
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Pry);
            }
            set
            {
                if (Pry == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Pry);
                NotifyPropertyChanged();
            }
        }
        public ushort Gella
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Gella);
            }
            set
            {
                if (Gella == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Gella);
                NotifyPropertyChanged();
            }
        }
        public ushort Xp
        {
            get
            {
                return ReadUnsignedShort(EnemyOffsets.Xp);
            }
            set
            {
                if (Xp == value)
                    return;
                WriteUnsignedShort(value, EnemyOffsets.Xp);
                NotifyPropertyChanged();
            }
        }
        public byte RawAttackElement
        {
            get
            {
                return ReadByte(EnemyOffsets.AttackElement);
            }
            set
            {
                if (RawAttackElement == value)
                    return;
                WriteByte(value, EnemyOffsets.AttackElement);
                NotifyPropertyChanged();
            }
        }
        public byte RawWeakness
        {
            get
            {
                return ReadByte(EnemyOffsets.WeaknessElement);
            }
            set
            {
                if (RawWeakness == value)
                    return;
                WriteByte(value, EnemyOffsets.WeaknessElement);
                NotifyPropertyChanged();
            }
        }
        public byte RawResistance
        {
            get
            {
                return ReadByte(EnemyOffsets.ResistanceElement);
            }
            set
            {
                if (RawResistance == value)
                    return;
                WriteByte(value, EnemyOffsets.ResistanceElement);
                NotifyPropertyChanged();
            }
        }
        public byte RawAbsorb
        {
            get
            {
                return ReadByte(EnemyOffsets.AbsorbElement);
            }
            set
            {
                if (RawAbsorb == value)
                    return;
                WriteByte(value, EnemyOffsets.AbsorbElement);
                NotifyPropertyChanged();
            }
        }
        public byte RawImmunity
        {
            get
            {
                return ReadByte(EnemyOffsets.ImmunityElement);
            }
            set
            {
                if (RawImmunity == value)
                    return;
                WriteByte(value, EnemyOffsets.ImmunityElement);
                NotifyPropertyChanged();
            }
        }
        public byte DropId
        {
            get
            {
                return ReadByte(EnemyOffsets.DropId);
            }
            set
            {
                if (DropId == value)
                    return;
                WriteByte(value, EnemyOffsets.DropId);
                NotifyPropertyChanged();
            }
        }

        public byte DropRate
        {
            get
            {
                return ReadByte(EnemyOffsets.DropRate);
            }
            set
            {
                if (DropRate == value)
                    return;
                WriteByte(value, EnemyOffsets.DropRate);
                NotifyPropertyChanged();
            }
        }
        public byte StealId
        {
            get
            {
                return ReadByte(EnemyOffsets.StealId);
            }
            set
            {
                if (StealId == value)
                    return;
                WriteByte(value, EnemyOffsets.StealId);
                NotifyPropertyChanged();
            }
        }

        public byte StealRate
        {
            get
            {
                return ReadByte(EnemyOffsets.StealRate);
            }
            set
            {
                if (StealRate == value)
                    return;
                WriteByte(value, EnemyOffsets.StealRate);
                NotifyPropertyChanged();
            }
        }

        public byte Attack1Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack1Id);
            }
            set
            {
                if (Attack1Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack1Id);
            }
        }

        public byte Attack1Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack1Probability);
            }
            set
            {
                if (Attack1Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack1Probability);
            }
        }

        public byte Attack2Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack2Id);
            }
            set
            {
                if (Attack2Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack2Id);
            }
        }

        public byte Attack2Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack2Probability);
            }
            set
            {
                if (Attack2Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack2Probability);
            }
        }

        public byte Attack3Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack3Id);
            }
            set
            {
                if (Attack3Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack3Id);
            }
        }

        public byte Attack3Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack3Probability);
            }
            set
            {
                if (Attack3Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack3Probability);
            }
        }

        public byte Attack4Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack4Id);
            }
            set
            {
                if (Attack4Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack4Id);
            }
        }

        public byte Attack4Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack4Probability);
            }
            set
            {
                if (Attack4Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack4Probability);
            }
        }

        public byte Attack5Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack5Id);
            }
            set
            {
                if (Attack5Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack5Id);
            }
        }

        public byte Attack5Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack5Probability);
            }
            set
            {
                if (Attack5Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack5Probability);
            }
        }

        public byte Attack6Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack6Id);
            }
            set
            {
                if (Attack6Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack6Id);
            }
        }

        public byte Attack6Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack6Probability);
            }
            set
            {
                if (Attack6Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack6Probability);
            }
        }

        public byte Attack7Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack7Id);
            }
            set
            {
                if (Attack7Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack7Id);
            }
        }

        public byte Attack7Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack7Probability);
            }
            set
            {
                if (Attack7Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack7Probability);
            }
        }

        public byte Attack8Id
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack8Id);
            }
            set
            {
                if (Attack8Id == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack8Id);
            }
        }

        public byte Attack8Probability
        {
            get
            {
                return ReadByte(EnemyOffsets.Attack8Probability);
            }
            set
            {
                if (Attack8Probability == value)
                    return;
                WriteByte(value, EnemyOffsets.Attack8Probability);
            }
        }

        public ObservableCollection<string> AttackElements { get; }
        public ObservableCollection<string> ElementalWeaknesses { get; }
        public ObservableCollection<string> ElementalResistances { get; }
        public ObservableCollection<string> ElementalAbsorbs { get; }
        public ObservableCollection<string> ElementalImmunities { get; }

        public EnemyObject(byte[] rawData, long offset) : base(rawData, offset)
        {
            AttackElements = new ObservableCollection<string>();
            ElementalWeaknesses = new ObservableCollection<string>();
            ElementalResistances = new ObservableCollection<string>();
            ElementalAbsorbs = new ObservableCollection<string>();
            ElementalImmunities = new ObservableCollection<string>();
            ParseData();
        }

        private void ParseData()
        {
            Id = ReadByte(EnemyOffsets.Id);
            foreach (var s in DataInterpreter.ConvertElements(RawAttackElement))
                AttackElements.Add(s);
            foreach (var s in DataInterpreter.ConvertElements(RawWeakness))
                ElementalWeaknesses.Add(s);
            foreach (var s in DataInterpreter.ConvertElements(RawResistance))
                ElementalResistances.Add(s);
            foreach (var s in DataInterpreter.ConvertElements(RawAbsorb))
                ElementalAbsorbs.Add(s);
            foreach (var s in DataInterpreter.ConvertElements(RawImmunity))
                ElementalImmunities.Add(s);
        }

        public override bool Equals(object obj)
        {
            var enemy = obj as EnemyObject;
            if (enemy == null)
                return false;
            return GetHashCode() == enemy.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
