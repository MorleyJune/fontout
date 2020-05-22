import codecs
import glob

csv = open( "exaList.csv", "w")

exaList = glob.glob('./*.exa')
for exaFile in exaList:
    with codecs.open(exaFile, 'r', 'shift_jis') as f:
        # とりあえず行ごとの文字列をリストとして格納
        lines = f.readlines()
        # 改行コードを消す
        lines_strip = [line.strip() for line in lines]
        # 'font='で始まる行だけを抜き取る
        lines_font = [line for line in lines_strip if line.startswith('font')]
        # リストから文字列に変換、と同時に'font='を抜く
        fontline = ''.join(lines_font).strip('font=')
        # パスの中にある余計な文字列を取り除いて出力
        print(exaFile.strip('.\\').strip('.exa') + ': ' + fontline)
        # こっちはcsvへの書き込み
        writeStr = exaFile.strip('.\\').strip('.exa')+',' +fontline
        # 最後以外は改行を含める
        if(exaFile == exaList[-1]):
            csv.write(writeStr)
        else:
            csv.write(writeStr+'\n')
csv.close()
