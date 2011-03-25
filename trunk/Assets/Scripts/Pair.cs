//from http://stackoverflow.com/questions/166089/what-is-c-analog-of-c-stdpair

using MurderData;

public class Pair<T, U> {
	
	public T First { get; set; }
    public U Second { get; set; }
	
    public Pair() {
    }

    public Pair(T first, U second) {
        this.First = first;
        this.Second = second;
    }

};
