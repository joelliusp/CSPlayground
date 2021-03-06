﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    public class MixinData
    {
        private readonly Dictionary<Type, int> mixinPositions = new Dictionary<Type, int>();
        private readonly List<object> mixinsImpl = new List<object>();


        public IEnumerable<object> Mixins
        {
            get { return mixinsImpl; }
        }

        public IEnumerable<Type> MixinInterfaces
        {
            get { return this.mixinPositions.Keys; }
        }

        public MixinData(IEnumerable<object> mixinInstances)
        {
            if(mixinInstances != null)
            {
                List<Type> sortedMixedInterfaceTypes = new List<Type>();
                Dictionary<Type, object> interfaces2Mixin = new Dictionary<Type, object>();

                foreach(object mixin in mixinInstances)
                {
                    Type[] mixinInterfaces =  mixin.GetType().GetInterfaces();

                    foreach(Type inter in mixinInterfaces)
                    {
                        sortedMixedInterfaceTypes.Add(inter);

                        if(interfaces2Mixin.ContainsKey(inter))
                        {
                            throw new ArgumentException("key already exists");
                        }
                        interfaces2Mixin[inter] = mixin;
                    }
                }

                sortedMixedInterfaceTypes.Sort((x, y) => x.FullName.CompareTo(y.FullName));

                for(int i = 0;i < sortedMixedInterfaceTypes.Count;i++)
                {
                    Type mixinInterface = sortedMixedInterfaceTypes[i];
                    object mixin =  interfaces2Mixin[mixinInterface];

                    mixinPositions[mixinInterface] = i;
                    mixinsImpl.Add(mixin);
                }
                
            }
        }

        public bool ContainsMixin(Type mixinInterfaceType)
        {
            return this.mixinPositions.ContainsKey(mixinInterfaceType);
        }

        public object GetMixinInstance(Type mixinInterfaceType)
        {
            return this.mixinsImpl[mixinPositions[mixinInterfaceType]];
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this,obj))
            {
                return true;
            }

            MixinData other = obj as MixinData;
            if(ReferenceEquals(other,null))
            {
                return false;
            }

            if(this.mixinsImpl.Count != other.mixinsImpl.Count)
            {
                return false;
            }

            for(int i = 0; i < mixinsImpl.Count; i++)
            {
                if(mixinsImpl[i].GetType() != other.mixinsImpl.GetType())
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                foreach(object mixinImplementation in this.mixinsImpl)
                {
                    hashCode = 29 * hashCode + mixinImplementation.GetType().GetHashCode();
                }
                return hashCode;
            }
        }
    }
}
