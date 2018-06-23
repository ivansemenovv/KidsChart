import { Component, Inject } from '@angular/core';
//import { Component, interval } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public dateTime = Date.now();
  public forecasts: WeatherForecast[];
  public responsibilities: Responsibility[];
  private _http: HttpClient;
  private _baseUrl: string;
  private _intervalId = 0;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._intervalId = window.setInterval(() => { this.refresh(); }, 5000);
  }

  public refresh() {
    this._http.get<WeatherForecast[]>(this._baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    this._http.get<Responsibility[]>(this._baseUrl + 'api/SampleData/GetResponsibilities').subscribe(result => {
      this.responsibilities = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Responsibility
{
  description: string;
  isDone: boolean;
}
