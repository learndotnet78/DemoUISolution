import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CustomerDetail } from './customer-detail.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerDetailService {

  url :string = environment.apiBaseUrl + 'api/Customer';
  customerlist : CustomerDetail[] =[];
  customerData : CustomerDetail = new CustomerDetail;
  deleted : boolean = false;

  constructor(private http: HttpClient ) { }

  getCustomerList() : void {

    this.http.get(this.url)
    .subscribe({
      next : res => {
        this.customerlist = res as CustomerDetail[];
      },
      error : err => { console.log(err)}
    });
  }

  getCustomerByID(customerID : number) : void {

    this.http.get(this.url + '/' + customerID)
    .subscribe({
      next : res => {
        this.customerData = res as CustomerDetail;
      },
      error : err => { console.log(err)}
    });
  }

  postCustomer(){
    return this.http.post(this.url + '/AddCustomer', this.customerData);
  }

  putCustomer(){
    return this.http.post(this.url + '/UpdateCustomer', this.customerData);
  }

  deleteCustomer(customerID : number) : boolean {

    this.http.delete(this.url + '/' + customerID)
    .subscribe({
      next : res => {
        this.deleted = res as boolean;
      },
      error : err => { console.log(err)}
    });
    console.log(this.deleted);
    return this.deleted;
  }

}
