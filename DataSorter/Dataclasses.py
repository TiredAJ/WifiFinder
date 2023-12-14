import datetime
from dataclasses import dataclass, field
from dataclass_wizard import JSONWizard

@dataclass
class WifiItem(JSONWizard):
    BSSID: str
    SSID: str
    _RSSI: int
    LastUpdated: datetime
    _Distance: float
        
    def __str__(self):
        return f"""BSSID: {self.BSSID}, SSID: {self.SSID}, RSSI: {self.RSSI}dbm, 
                Last Up: {self.LastUpdated}, Distance: {self.Distance}m"""

@dataclass
class Record(JSONWizard):
    LastUpdated: datetime
    Data: dict[str, WifiItem] = field(default_factory=dict)
        
    def __str__(self):
        return f"Name: {self.LastUpdated}, Now: {len(self.Data)}"
    
    def __init__(self, _LP: datetime, _Data: dict[str, WifiItem]):
        self.LastUpdated = _LP
        self.Data = _Data
    
    
@dataclass
class Snapshot(JSONWizard):
    Data: list[Record]
    
    def __str__(self) -> str:
        return f"No Records: {len(self.Data)}"