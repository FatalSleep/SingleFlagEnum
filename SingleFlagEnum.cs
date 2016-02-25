public class SingleFlagEnum<T> where T : struct, IComparable {
    /// <summary>Gets the flag set for this object.</summary>
    private T Flag { get; set; }

    /// <summary>Constructs a RestrictedEnum<T> with the default value of T.</summary>
    public SingleFlagEnum() {
        if(!typeof(T).IsEnum)
            throw new NotSupportedException();
    }

    /// <summary>Constructs a RestrictedEnum<T> with the provided flag of type T.</summary>
    /// <param name="flag">Flag to set.</param>
    public SingleFlagEnum(T flag) : this() {
        SetFlag(flag);
    }

    /// <summary>Parses a member name into a member of type T if possible, otherwise null is returned.</summary>
    /// <param name="name">Name of the member to parse.</param>
    /// <returns>A nullable value of type T of the member that was parsed.</returns>
    public static SingleFlagEnum<T> Parse(string name) {
        SingleFlagEnum<T> result = null;
        T value;

        if(Enum.TryParse<T>(name, out value)) {
            result = new SingleFlagEnum<T>();
            result.SetFlag(value);
        }

        return result;
    }

    /// <summary>Attempts to set the defined flag if it exists in the Enum.</summary>
    /// <param name="flag">Enum flag to set.</param>
    /// <returns>True if the flag was set, false if the flag does not exist or the flag is already set.</returns>
    public bool SetFlag(T flag) {
        bool isDefinedFlagNotSet = !Enum.Equals(Flag, flag) && Enum.IsDefined(typeof(T), flag);
        if(isDefinedFlagNotSet)
            Flag = flag;
        return isDefinedFlagNotSet;
    }

    /// <summary>Gets whether the provided flag is equal to the set flag.</summary>
    /// <param name="flag">Flag to check.</param>
    /// <returns></returns>
    public bool HasFlag(T flag) =>
        Enum.Equals(Flag, flag);

    /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns> A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes obj in the sort order. Zero This instance occurs in the same position in the sort order as obj. Greater than zero This instance follows obj in the sort order.</returns>
    public int CompareTo(SingleFlagEnum<T> obj) =>
        Flag.CompareTo(obj.Flag);

    /// <summary>Gets whether two objects contain the same flag.</summary>
    /// <param name="obj1">First object to compare.</param>
    /// <param name="obj2">Second object to compare.</param>
    /// <returns>True if the flag of each object is the same, else false.</returns>
    public static bool operator ==(SingleFlagEnum<T> obj1, SingleFlagEnum<T> obj2) =>
        obj1.Equals(obj2);

    /// <summary>Gets whether two objects do NOT contain the same flag.</summary>
    /// <param name="obj1">First object to compare.</param>
    /// <param name="obj2">Second object to compare.</param>
    /// <returns>True if the flag of each object is NOT the same, else false.</returns>
    public static bool operator !=(SingleFlagEnum<T> obj1, SingleFlagEnum<T> obj2) =>
        !obj1.Equals(obj2);

    /// <summary>Gets the type of T of this object.</summary>
    /// <returns>The underlying type of the enum of T of this generic object.</returns>
    public Type GetUnderlyingType() =>
        typeof(T);

    /// <summary>Gets whether two objects equal (they share the same reference).</summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if the provided object is equal to this object.</returns>
    public bool Equals(SingleFlagEnum<T> obj) {
        if(obj.GetType() != GetType())
            return false;
        return this == obj;
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object, else false.</returns>
    public override bool Equals(object obj) =>
        Equals((SingleFlagEnum <T>) obj);

    /// <summary>Gest the hash code for the current object.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() =>
        Flag.GetHashCode() | typeof(T).GetHashCode();
}