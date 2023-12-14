import json, datetime, time

from dataclasses import dataclass, field
from dataclass_wizard import *
from io import StringIO
from Dataclasses import *
from typing import cast

DataLoc = "E:\\GitHub\\_Individual Project\\WifiFinder-Survey\\ScanData\\J-Block\\Floor-1\\J1-East\\1.apdat"

Reader = open(DataLoc, mode='r')

Data = []

InData = json.loads(Reader.read())

for Di in InData:
    Temp = Di["LastUpdated"]
    
    print(Temp)
    Data.append(Record.from_dict(Di))
    time.sleep(2)

#print(len(Data))
#print(f"{type(Data)} - {type(Data[0])}")

#for El in Data:
#    print(len(Record(El).Data))

#print(Data[0])