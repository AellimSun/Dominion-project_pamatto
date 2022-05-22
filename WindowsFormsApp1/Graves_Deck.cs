using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    class Graves_Deck
    {

        // action buy 객체배열, hand 객체배열, drawdeck 객체 사용

        // 보낼 사람, 받는 사람을 각각 객체, 보낼 객체배열 만들고 퍼블릭하면 어떨까 구매, 덱 송신

        // 반복문 탈출조건
        // 턴이 끝났을때(action && buy =0 || AB doubleclick)
        // void Add (action buy 객체배열, hand객체배열) 덱 추가
        // 내 객체 배열 만들고 여기다가 action buy, hand 객체배열 붙이기

        // 셔플 메소드에서 셔플

        // public void Shuffle()

        // shuffle 메소드
        // drawdeck에 객체배열 송신
        // 내 객체배열 초기화

        static int Next(RNGCryptoServiceProvider random, int Num)
        {
            byte[] randomInt = new byte[Num];
            random.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }

        //static void Shuffle()
        //{
        //    int[] arr = { 1, 2, 3, 4, 5 };
        //    RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        //    arr = arr.OrderBy(x => Next(random,Num)).ToArray();
        //}
    }
}