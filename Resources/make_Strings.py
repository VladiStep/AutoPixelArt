import codecs

f = codecs.open('StringsEN.txt', 'r', 'UTF-8')
f_out = codecs.open('Strings_out.txt', 'w', 'UTF-8')

for line in f:
	s = line[:-2].split(' = ')
	#f_out.write(f'  <data name="{s[0]}" xml:space="preserve">\n    <value>{s[1]}</value>\n  </data>\n')
	f_out.write(f'  <data name="{s[0]}" xml:space="preserve">\n    <value />\n  </data>\n')
	
f_out.close()
f.close()