using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVirtualMachine.Libs {
    /// <summary>
    /// オペコード定義クラス
    /// https://www.mlab.im.dendai.ac.jp/~assist/PIC/appendix/instruction/
    /// </summary>
    class Opecode {
        /// <summary>
        /// 実行しない(オペランド無し)
        /// </summary>
        public const byte OPE_NOP = 0x0;

        /// <summary>
        /// 加算
        /// </summary>
        public const byte OPE_ADDWF= 0x1;

        /// <summary>
        /// 論理積(AND)演算
        /// </summary>
        public const byte OPE_ANDWF = 0x2;

        /// <summary>
        /// fを0クリア
        /// </summary>
        public const byte OPE_CLR= 0x3;

        /// <summary>
        /// レジスタを0クリア
        /// </summary>
        public const byte OPE_CLRW = 0x4;

        /// <summary>
        /// bit反転
        /// </summary>
        public const byte OPE_COMF = 0x5;

        /// <summary>
        /// デクリメント
        /// </summary>
        public const byte OPE_DECF = 0x6;

        /// <summary>
        /// デクリメント結果が0ならば次の命令スキップ
        /// </summary>
        public const byte OPE_DECFSZ = 0x7;

        /// <summary>
        /// デクリメント
        /// </summary>
        public const byte OPE_INCF = 0x8;

        /// <summary>
        /// デクリメント結果が0ならば次の命令スキップ
        /// </summary>
        public const byte OPE_INCFSZ = 0x9;

        /// <summary>
        /// 論理和演算
        /// </summary>
        public const byte OPE_IORWF = 0xA;

        /// <summary>
        /// 移動FからWまたはF自身へ格納
        /// </summary>
        public const byte OPE_MOVF = 0xB;

        /// <summary>
        /// 移動WからF
        /// </summary>
        public const byte OPE_MOVWF = 0xC;

        /// <summary>
        /// 1bit左へシフト
        /// </summary>
        public const byte OPE_RLF = 0xD;

        /// <summary>
        /// 1bit右へシフト
        /// </summary>
        public const byte OPE_RRF = 0xE;

        /// <summary>
        /// 減算
        /// </summary>
        public const byte OPE_SUBWF = 0xF;

        /// <summary>
        /// Fの上位と下位を入れ替え
        /// </summary>
        public const byte OPE_SWAPF = 0x10;

        /// <summary>
        /// 排他的論理和
        /// </summary>
        public const byte OPE_XORWF = 0x11;

        /// <summary>
        /// ｆのbビット目を0クリア
        /// </summary>
        public const byte OPE_BCF = 0x20;

        /// <summary>
        /// fのbビット目を1にする
        /// </summary>
        public const byte OPE_BSF = 0x28;

        /// <summary>
        /// 
        /// </summary>
        public const byte OPE_BTFSC = 0x24;

        /// <summary>
        /// 
        /// </summary>
        public const byte OPE_BTFSS = 0x2C;

        /// <summary>
        /// 定数加算
        /// </summary>
        public const byte OPE_ADDLW = 0x16;

        /// <summary>
        /// 定数論理積(AND)
        /// </summary>
        public const byte OPE_ANDLW = 0x17;

        /// <summary>
        /// 定数論理和(OR)
        /// </summary>
        public const byte OPE_IORWL = 0x18;

        /// <summary>
        /// 定数移動
        /// </summary>
        public const byte OPE_MOVLW = 0x19;

        /// <summary>
        /// 定数減算
        /// </summary>
        public const byte OPE_SUBLW = 0x1A;

        /// <summary>
        /// 定数排他的論理和　Wへ格納
        /// </summary>
        public const byte OPE_XORLW = 0x1B;

        /// <summary>
        /// サブルーチンkへジャンプ
        /// </summary>
        public const byte OPE_CALL = 0x1C;

        /// <summary>
        /// K番地へジャンプ
        /// </summary>
        public const byte OPE_GOTO = 0x1D;

        /// <summary>
        /// 割り込み許可で戻る
        /// </summary>
        public const byte OPE_RETFIE = 0x1E;

        /// <summary>
        /// WにKを格納して戻る
        /// </summary>
        public const byte OPE_RETLW = 0x1F;

        /// <summary>
        /// サブルーチンから戻る
        /// </summary>
        public const byte OPE_RETURN = 0x12;
        /*
        /// <summary>
        /// ウォッチドタイマクリア
        /// </summary>
        public const byte OPE_CLRWDT = 0x13;

        /// <summary>
        /// スリープモード
        /// </summary>
        public const byte OPE_SLEEP = 0x14;
        */
    }
}
