import { Component, OnInit } from '@angular/core';
import { CustomerDetailService } from '../shared/customer-detail.service';
import { CustomerDetail } from '../shared/customer-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrl: './customer-details.component.css'
})
export class CustomerDetailsComponent implements OnInit {
 customers: CustomerDetail[] = [];
 returnVal : boolean = false;
 constructor( public service : CustomerDetailService, private toastr : ToastrService){

 }
  ngOnInit(): void {
   this.service.getCustomerList();
  }

  deleteCustomer(customerID : number) {
    console.log(customerID);
    this.returnVal = this.service.deleteCustomer(customerID);
    if (this.returnVal){
      this.toastr.success("Customer deleted successsfully","Customer Details");
    }
    else{
      this.toastr.error("Customer was not deleted due to error","Customer Details");
    }
  }
}
