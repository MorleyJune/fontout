import codecs
import glob

csv = open( "exaList.csv", "w")

exaList = glob.glob('./*.exa')
for exaFile in exaList:
    with codecs.open(exaFile, 'r', 'shift_jis') as f:
        lines = f.readlines()
        # 改行コードを消す
        lines_strip = [line.strip() for line in lines]
        lines_font = [line for line in lines_strip if line.startswith('font')]
        fontline = ''.join(lines_font).strip('font=')
        print(exaFile.strip('.\\').strip('.exa') + ': ' + fontline)
        writeStr = exaFile.strip('.\\').strip('.exa')+',' +fontline
        if(exaFile == exaList[-1]):
            csv.write(writeStr)
        else:
            csv.write(writeStr+'\n')
f.close()