using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVirtualMachine.Libs {
    /// <summary>
    /// CPUクラス(演算装置、制御装置)
    /// </summary>
    class Cpu {
        /// <summary>
        /// プログラムカウンタ
        /// </summary>
        protected ulong pc;

        /// <summary>
        /// オペコード
        /// </summary>
        protected byte opecode;

        /// <summary>
        /// オペランド
        /// </summary>
        protected byte operand;

        /// <summary>
        /// ワーキングレジスタ
        /// </summary>
        protected byte working;

        /// <summary>
        /// サブルーチンの戻り先
        /// </summary>
        protected ulong swap_call_pc;

        /// <summary>
        /// メモリのハンドル
        /// </summary>
        protected Memory mem;

        public Cpu( Memory mem)
        {
            this.mem = mem;
            pc = 0;
        }

        /// <summary>
        /// 実行
        /// </summary>
        public void Run()
        {
            for( pc = 0; pc < mem.GetSize();) {
                StepRun();
            }
        }

        /// <summary>
        /// ステップ実行
        /// </summary>
        public void StepRun()
        {
            byte d = 0;
            byte b = 0;
            ulong f = 0;
            opecode = GetOpecode( mem.Get( pc ++ ) );
            operand = mem.Get(pc++);
            switch (opecode){
                case Opecode.OPE_NOP:   //何もしない
                    break;

                case Opecode.OPE_ADDWF://加算 W + f → W か f へ格納
                    d = GetDbit( operand );
                    f = GetAddress( operand );
                    working += mem.Get( f );
                    if( d == 0 ) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_ANDWF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(working & mem.Get(f));
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_CLR:
                    f = GetAddress(operand);
                    mem.Set(f, 0);
                    break;

                case Opecode.OPE_CLRW:
                    working = 0;
                    break;

                case Opecode.OPE_COMF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(~mem.Get(f));
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_DECF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(mem.Get(f) - 1);
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_DECFSZ:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(mem.Get(f) - 1);
                    if (working == 0) pc += 2;
                    break;


                case Opecode.OPE_INCF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(mem.Get(f) + 1);
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_INCFSZ:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(mem.Get(f) + 1);
                    if (working == 0) pc += 2;
                    break;

                case Opecode.OPE_IORWF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(mem.Get(f) | working);
                    break;

                case Opecode.OPE_MOVF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    if( d == 1) {
                        working = mem.Get(f);
                    } else {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_MOVWF:
                    f = GetAddress(operand);
                    mem.Set(f, working);
                    break;

                case Opecode.OPE_RLF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(working << 1);
                    if(d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_RRF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(working >> 1);
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_SUBWF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working -= mem.Get(f);
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_SWAPF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = mem.Get(f);
                    working = (byte)((working & 0b1100 >> 2 ) | (working & 0b0011 << 2 ));
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_XORWF:
                    d = GetDbit(operand);
                    f = GetAddress(operand);
                    working = (byte)(working ^ mem.Get(f));
                    if (d == 0) {
                        mem.Set(f, working);
                    }
                    break;

                case Opecode.OPE_BCF:
                    f = GetAddress(operand);
                    b = GetBval(opecode, operand);
                    mem.Set(f, (byte)(mem.Get(f) & ( ~(0b1 << b) ) ));
                    break;

                case Opecode.OPE_BSF:
                    f = GetAddress(operand);
                    b = GetBval(opecode, operand);
                    mem.Set(f, (byte)(mem.Get(f) | (0b1 << b)));
                    break;

                case Opecode.OPE_BTFSC:
                    f = GetAddress(operand);
                    b = GetBval(opecode, operand);
                    if( (int)(mem.Get(f) & (0b1 << b)) == 0 ) {
                        pc += 2;
                    }
                    break;

                case Opecode.OPE_BTFSS:
                    f = GetAddress(operand);
                    b = GetBval(opecode, operand);
                    if ((int)(mem.Get(f) & (0b1 << b)) != 0) {
                        pc += 2;
                    }
                    break;


                case Opecode.OPE_ADDLW:
                    working += operand;
                    break;

                case Opecode.OPE_ANDLW:
                    working &= operand;
                    break;

                case Opecode.OPE_IORWL:
                    working |= operand;
                    break;

                case Opecode.OPE_MOVLW:
                    working = operand;
                    break;

                case Opecode.OPE_SUBLW:
                    working -= operand;
                    break;

                case Opecode.OPE_XORLW:
                    working = (byte)(working ^ operand);
                    break;

                case Opecode.OPE_CALL:
                    swap_call_pc = pc;
                    pc = operand;
                    break;

                case Opecode.OPE_GOTO:
                    pc = operand;
                    break;

                case Opecode.OPE_RETLW:
                    working = operand;
                    pc = swap_call_pc;
                    break;

                case Opecode.OPE_RETURN:
                    pc = swap_call_pc;
                    break;

                default:
                    throw new Exception("命令にないOpecodeを読み込んだ");
                    
            }
        }

        /// <summary>
        /// d値の取得
        /// </summary>
        /// <param name="code">オペランド</param>
        /// <returns></returns>
        protected byte GetDbit( byte code )
        {
            byte ret = (byte)((int)code & 0b01000000);
            ret = (byte)((int)code >> 7);
            return ret;
        }

        /// <summary>
        /// Bbitの取得
        /// </summary>
        /// <param name="code">オペコード</param>
        /// <returns></returns>
        protected byte GetBbit( byte code)
        {
            return (byte)( code & 0b111 );
        }

        /// <summary>
        /// B値の取得
        /// </summary>
        /// <param name="code">オペーコード</param>
        /// <param name="opr">オペランド</param>
        /// <returns></returns>
        protected byte GetBval( byte code, byte opr)
        {
            return (byte)((code & 0b11 << 1 ) | (opr & 0b1000000 >> 7));
        }

        /// <summary>
        /// オペーコード取得
        /// </summary>
        /// <param name="code">メモリの値</param>
        /// <returns></returns>
        protected byte GetOpecode( byte code)
        {
            byte ret = 0;
            byte b = GetBbit(code);
            if( b == 1) {
                ret = (byte)(code & 0b111100);
            } else {
                ret = code;
            }
            return ret;
        }

        /// <summary>
        /// コードからアドレスを取得
        /// </summary>
        /// <param name="code">オペランド</param>
        /// <returns></returns>
        protected byte GetAddress( byte code )
        {
            return (byte)((int)code & 0b01111111);
        }
    }
}
