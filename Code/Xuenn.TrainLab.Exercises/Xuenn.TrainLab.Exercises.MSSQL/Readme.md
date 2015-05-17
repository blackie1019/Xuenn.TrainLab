#掛載資料庫檔案

1. 打開SSMS(SQL Server Management Studio) 或 SSMSE(SQL Server Management Studio Express).
2. 在Object Explorer裡的 "Databases"上按右鍵.
3. 從文件清單中選取要附加的檔案.
4. 選擇 NorthwindChinese.mdf 按附加然後按確定
5. 選擇 Answers.mdf 按附加然後按確定.
6. 再按確定

#修改程式連線
1. 修改 Web.config 第14行
	
	connectionString="Data source=.;Initial Catalog=NorthwindChinese;User id=jane;Password=abc123"

2. 修改 ., jane, abc123 為你自己的帳號密碼.
3. 用VISUAL STUDIO 打開 SQLExercises.sln 然後執行.
4. 現在可以看到所有練習題目 :-)
5. 將你的SQL語法填入空格中並按送出. 經過檢查後將會跳出答對與否的訊息.

#注意事項:
	- 資料庫名稱必須為 "NorthwindChinese" 與 "Answers", 否則將會發生錯誤.
	- 禁止修改Answers資料庫裡的任何資料.
	- 查詢結果的資料順序不能錯否則算不對
	- 除非被要求否則不要做任何資料表的修改.
	- 22 到 25 題 Trigger名稱與欄位和資料必須與題目一樣.
	- 如果修改了資料表後想要復原它,請在空格中不要連任何的文字案送出. 資料表將會自動復原.

