import React, { Component } from 'react';
import PriceDisplay from '../components/PriceDisplay';
import moment from 'moment/moment.js';
import axios from 'axios';

export default class Carpark extends Component {
    constructor(props) {
        super(props);
        this.state = {
            startDate: '',
            endDate: '',
            loading: true,
            priceResult: {}
        };
        this.handleChange = this.handleChange.bind(this);
        this.validateAndCalculatePrice = this.validateAndCalculatePrice.bind(this);
        this.calculateFinalPrice = this.calculateFinalPrice.bind(this);
    }

    handleChange(event) {
        const { target: { name, value } } = event
        this.setState({
            [name]: value
        })
    }

    validateAndCalculatePrice() {
        console.log(this.state.startDate);
        console.log(this.state.endDate);

        if ((this.state.startDate === null || this.state.startDate === "" || this.state.startDate === undefined)
            || (this.state.endDate === null || this.state.endDate === "" || this.state.startDate === undefined)) {
            alert("Please enter the start or end of car park timings");
        }
        else if (moment(this.state.startDate).isBefore(moment())) {
            alert("start time needs to be after current time");
        }
        else if (moment(this.state.endDate).isBefore(moment()) || moment(this.state.endDate).isBefore(moment(this.state.startDate))) {
            alert("end time needs to be after current or start time");
        }
        else {
            let start = moment(this.state.startDate).format('YYYY-MM-DD, h:mm:ss a');
            let end = moment(this.state.endDate).format('YYYY-MM-DD, h:mm:ss a');
            this.calculateFinalPrice(start, end);
        }
    }

    calculateFinalPrice(start, end) {
        var self = this;
        axios({
            method: 'post',
            url: '/api/CalculatePrice/FinalPriceCarpark',
            data: { "start": start, "end": end }
        }).then(function (response) {
            debugger;
            console.log(response.data.priceObject);
            self.setState({
                priceResult: response.data.priceObject
            })

        }).catch(function (error) {
            console.log(error);
        });
    }

    render() {
        let { priceResult } = this.state;
        let startInput = <input name="startDate" placeholder="End date" type="datetime-local" value={this.state.startDate} onChange={(event) => this.handleChange(event)} />;
        let endInput = <input name="endDate" placeholder="End date" type="datetime-local" value={this.state.endDate} onChange={(event) => this.handleChange(event)} />;

        return (
            <div className="container">
                <h1 style={{ "marginBottom": "15px" }} className="list-group-item list-group-item-action active">'TALL-YO' - INVOICE</h1>
                <div className="row">
                    <div className="col-sm-6">
                        <div className="jumbotron">
                            <div className="card">
                                <div className="card-body">
                                    <p className="card-text"> ENTRY TIME</p>
                                    {startInput}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div className="jumbotron">
                            <div className="card">
                                <div className="card-body">
                                    <p className="card-text"> EXIT TIME</p>
                                    {endInput}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div >
                    <input type="button" className=" btn btn-primary btn-lg" onClick={this.validateAndCalculatePrice} value="Generate Invoice " />
                </div>
                <div style={{ "marginTop": "15px" }}>
                    <PriceDisplay priceResult={priceResult} />
                </div>
            </div>
        );
    }
}
