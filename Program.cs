using System;

namespace Queen {
	class Queen {
		private static int frequency;
		private static int count;
		private static int calc;
		private static int x, y;
		private static bool result;
		private static bool[,] position;

		public Queen(int data) {  //建構函式
			frequency = data;
			position = new bool[frequency, frequency];
		}

		private void Initial() {  //初始化
			for(int i = 0; i < frequency; i++) {
				for(int j = 0; j < frequency; j++) {
					position[i, j] = false;
				}
			}

			count = 0;
		}

		private int AbsoluteValue(int value) {  //return絕對值
			return value < 0 ? 0 - value : value;
		}

		private bool Check() {  //檢查
			bool status = true;

			if(status) {  //檢查Y軸
				for(int i = 0; i < y; i++) {
					if(position[i, x]) {
						status = false;
						break;
					}
				}
			}

			if(status) {  //檢查斜線(左上右下↘)
				int start = y - x;
				int headY = start > 0 ? start : 0;
				int headX = start < 0 ? AbsoluteValue(start) : 0;
				int end = start > 0 ? x : y;
				for(int i = 0; i < end; i++) {
					if(position[i + headY, i + headX]) {
						status = false;
						break;
					}
				}
			}

			if(status) {  //檢查斜線(右上左下↙)
				int start = y + x;
				int headY = start > frequency - 1 ? start - (frequency - 1) : 0;
				int headX = start < frequency - 1 ? start : frequency - 1;
				int end = start < frequency ? y : frequency - x;
				for(int i = 0; i < end; i++) {
					if(position[i + headY, headX - i]) {
						status = false;
						break;
					}
				}
			}

			return status;
		}

		private void Print() {  //顯示
			for(int i = 0; i < frequency; i++) {
				for(int j = 0; j < frequency; j++) {
					System.Console.Write(position[i, j] ? "╳" : "－");
				}
				System.Console.Write("\n");
			}
			System.Console.Write("calc:" + calc + "\n");
		}

		public void Execute() {  //執行
			Random rnd = new Random();

			do {
				Initial();

				for(y = 0; y < frequency - 1; y++) {
					do {
						x = rnd.Next(0, frequency);
						result = Check();
						if(result) {
							position[y, x] = true;
							count++;
						} else {
							for(x = 0; x < frequency; x++) {
								if(Check()) {
									result = false;
									break;
								} else {
									result = true;
								}
							}
						}
					} while(!result);
				}

				y = frequency - 1;

				for(x = 0; x < frequency; x++) {
					if(Check()) {
						position[y, x] = true;
						count++;
						break;
					}
				}
				calc++;
			} while(count < frequency);
			Print();
			calc = 0;
		}
	}

	class Program {
		static int input;

		static void Input() {
			System.Console.Write("Enter a positive integer:");
			input = int.Parse(System.Console.ReadLine());
		}

		static void Main(string[] args) {
			Input();
			while(input > 3) {
				Queen qu = new Queen(input);
				qu.Execute();
				Input();
			}

			System.Console.Write("\nPress\0anykey\0to\0exit");
			System.Console.ReadKey();
		}
	}
}
