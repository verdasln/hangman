using System;                     // Giriş/çıkış işlemleri (Console.WriteLine, Console.ReadKey vs.) için gerekli kütüphane
using System.Collections.Generic; // List<T> gibi koleksiyonlar için gerekli kütüphane

class Program
{
    static void Main()
    {
        // Tahmin edilmesi gereken gizli kelime
        string secretWord = "apple";

        // Kullanıcıya gösterilecek olan gizli kelimenin '_' (alt çizgi) ile gizlenmiş hali
        // örnek: "apple" kelimesi için başlangıçta "_____" olarak gösterilecek
        char[] guessedWord = new string('_', secretWord.Length).ToCharArray();

        // Kullanıcının yapabileceği maksimum yanlış tahmin sayısı
        int maxTries = 6;

        // Şu ana kadar yapılan yanlış tahminlerin sayısı
        int wrongGuesses = 0;

        // Kullanıcının daha önce tahmin ettiği harfleri tutan liste
        List<char> guessedLetters = new List<char>();

        // Oyunun başlangıcı
        Console.WriteLine("Welcome to Hangman!"); // Kullanıcıya hoş geldin mesajı

        // Oyun döngüsü: Kullanıcı ya kazanana kadar ya da hakkı bitene kadar devam eder
        while (wrongGuesses < maxTries && new string(guessedWord) != secretWord)
        {
            // Kullanıcıya şu ana kadar bildiği harfleri göster
            Console.WriteLine("\nWord: " + new string(guessedWord));

            // Daha önce tahmin ettiği harfleri göster
            Console.WriteLine("Guessed letters: " + string.Join(", ", guessedLetters));

            // Kullanıcıdan yeni bir harf tahmini al
            Console.Write("Enter a letter: ");
            char guess = Char.ToLower(Console.ReadKey().KeyChar); // Girilen harfi küçük harfe çeviriyoruz
            Console.WriteLine();

            // Harf mi kontrolü (rakam ya da sembol olmamalı)
            if (!Char.IsLetter(guess))
            {
                Console.WriteLine("Please enter a valid letter."); // Geçerli bir harf değilse uyar
                continue; // Bu turu atla ve tekrar başa dön
            }

            // Aynı harfi daha önce tahmin etti mi?
            if (guessedLetters.Contains(guess))
            {
                Console.WriteLine("You already guessed that letter."); // Daha önce girilmişse uyar
                continue; // Bu turu atla ve tekrar başa dön
            }

            // Yeni tahmini listeye ekle
            guessedLetters.Add(guess);

            // Doğru tahmin mi?
            if (secretWord.Contains(guess))
            {
                // Tahmin doğruysa, o harfin bulunduğu yerleri aç
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guess)
                    {
                        guessedWord[i] = guess; // Doğru harfi gizli kelimenin ilgili yerine yerleştir
                    }
                }
                Console.WriteLine("Correct!"); // Doğru tahmin mesajı
            }
            else
            {
                // Yanlış tahmin yapıldıysa sayacı artır
                wrongGuesses++;
                Console.WriteLine("Wrong! You have " + (maxTries - wrongGuesses) + " tries left."); // Kalan hakkı göster
            }
        }

        // Oyun bitince kazanıp kazanmadığını kontrol et
        if (new string(guessedWord) == secretWord)
        {
            Console.WriteLine("\nCongratulations! You guessed the word: " + secretWord); // Tebrik mesajı
        }
        else
        {
            Console.WriteLine("\nGame over. The word was: " + secretWord); // Oyun bitti, doğru kelimeyi göster
        }
    }
}