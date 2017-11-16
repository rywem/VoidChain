using System.Collections;
using System.Collections.Generic;
//using WebRtc.NET;
//using WebSocket4Net;
//using LitJson;
using System.Collections.Concurrent;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;


namespace VoidChainLib.P2P
{

    public class Tuple<T1, T2>
    {
        public T1 left;
        public T2 right;

        public Tuple(T1 left, T2 right)
        {
            this.left = left;
            this.right = right;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Tuple<T1, T2>))
            {
                Tuple<T1, T2> other = (Tuple<T1, T2>)obj;
                return other.left.Equals(left) && other.right.Equals(right);
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return left.GetHashCode() ^ right.GetHashCode();
        }
    }
    public class PeerClass : IDisposable
    {
        
        public PeerClass()
        {
        }
    }
}
