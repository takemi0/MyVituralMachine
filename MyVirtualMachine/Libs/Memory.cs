using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVirtualMachine.Libs {
    /// <summary>
    /// メモリクラス
    /// </summary>
    class Memory {
        /// <summary>
        /// メモリのハンドラ
        /// </summary>
        protected byte[] mem;

        /// <summary>
        /// メモリのサイズ
        /// </summary>
        protected ulong size;

        public Memory(ulong memory_size)
        {
            size = memory_size;

            mem = new byte[size];
            for (ulong n = 0; n < size; n++) mem[n] = 0;
        }

        public ulong GetSize()
        {
            return size;
        }

        /// <summary>
        /// adress番地のメモリの値を取得
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte Get( ulong address)
        {
            if (size < address) throw new Exception("メモリサイズを超えるアドレスを要求されました");
            return mem[address];
        }

        public void Set( ulong address, byte num)
        {
            if (size < address) throw new Exception("メモリサイズを超えるアドレスを要求されました");
            mem[address] = num;
        }
    }
}
